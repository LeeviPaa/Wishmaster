using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private PauseMenu _pauseMenu;

    public Events.EventFadeComplete OnMainMenuFadeComplete;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    public void FadeIn(SceneAction action)
    {
        _mainMenu.FadeToLevel(action);
    }

    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        switch (currentState)
        {
            case GameManager.GameState.PAUSED:
                _pauseMenu.gameObject.SetActive(true);
                break;

            default:
                _pauseMenu.gameObject.SetActive(false);
                break;
        }
    }
}