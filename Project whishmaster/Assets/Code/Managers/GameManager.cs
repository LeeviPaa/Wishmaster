using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        MAINMAP,
        EXPEDITION,
        PAUSED
    }

    #region fields
    /// <summary>
    /// Called when game state changes. First parameter is the new state, second parameter is the old state
    /// </summary>
    public EventGameState OnGameStateChanged;
    
    private List<AsyncOperation> _loadOperations;
    private GameState _currentGameState = GameState.MAINMAP;
    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        private set { _currentGameState = value; }
    }

    string _currentLevelName;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        _loadOperations = new List<AsyncOperation>();
        
        OnGameStateChanged.Invoke(GameState.MAINMAP, _currentGameState);
    }

    void Update()
    {
        if (_currentGameState == GameState.MAINMAP)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    

    #region scene management
    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);

            if (_loadOperations.Count == 0)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(_activeSceneIndex));
            }
        }
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        // Clean up level is necessary, go back to main menu
    }


    private int _activeSceneIndex;
    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + levelName);
            return;
        }

        _activeSceneIndex = SceneManager.GetSceneByName(levelName).buildIndex;
        ao.completed += OnLoadOperationComplete;
        _loadOperations.Add(ao);

        _currentLevelName = levelName;
    }

    public void UnloadLevel(string levelName)
    {
        SceneManager.UnloadSceneAsync(levelName);
    }
    public void UnloadLevel(int levelIndex)
    {
        SceneManager.UnloadSceneAsync(levelIndex);
    }
    #endregion

    #region game state management
    public void StartExpedition()
    {
        //find the current expedition scene to load
        //parameters for the expedition: day, position, etc.
        UpdateState(GameState.EXPEDITION);
        LoadLevel("ExpeditionTest1");

    }

    public void ReturnToMap()
    {
        UpdateState(GameState.MAINMAP);
        UIManager.Instance.FadeIn(SceneAction.MissionMap);
    }

    public void LoadMapScene()
    {
        LoadLevel("MissionMap");
    }

    public void TogglePause()
    {
        UpdateState(_currentGameState == GameState.EXPEDITION ? GameState.PAUSED : GameState.EXPEDITION);
    }
    
    void UpdateState(GameState state)
    {
        GameState previousGameState = _currentGameState;
        _currentGameState = state;

        switch (CurrentGameState)
        {
            case GameState.MAINMAP:
                // Initialize any systems that need to be reset
                Time.timeScale = 1.0f;
                break;

            case GameState.EXPEDITION:
                //  Unlock player, enemies and input in other systems, update tick if you are managing time
                Time.timeScale = 1.0f;
                break;

            case GameState.PAUSED:
                // Pause player, enemies etc, Lock other input in other systems
                Time.timeScale = 0.0f;
                break;

            default:
                break;
        }

        OnGameStateChanged.Invoke(_currentGameState, previousGameState);
    }

    public void QuitGame()
    {
        // Clean up application as necessary
        // Maybe save the players game

        Debug.Log("[GameManager] Quit Game.");

        Application.Quit();
    }
#endregion

    /// <summary>
    /// Called when game state changes. First parameter is the new state, second parameter is the old state
    /// </summary>
    [System.Serializable] public class EventGameState : UnityEvent<GameState, GameState> { }
}
