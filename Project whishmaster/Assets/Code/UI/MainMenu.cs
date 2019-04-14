using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneAction
{
    Expedition,
    MainMenu,
    MissionMap
}

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animation _mainMenuAnimator;
    [SerializeField] private AnimationClip _fadeOutAnimation;
    [SerializeField] private AnimationClip _fadeInAnimation;

    public Events.EventFadeComplete OnFadeComplete;
    private SceneAction _levelToLoad = SceneAction.Expedition;
    
    private void Start()
    {
        FadeIn();
    }

    public void StartExpedition()
    {
        FadeOut();
        _levelToLoad = SceneAction.Expedition;
    }

    public void FadeToLevel(SceneAction action)
    {
        FadeOut();
        _levelToLoad = action;
    }

    public void OnFadeOutComplete()
    {
        OnFadeComplete.Invoke(false);

        print(_levelToLoad);
        switch(_levelToLoad)
        {
            case SceneAction.Expedition:
                GameManager.Instance.StartExpedition();
                break;
            case SceneAction.MainMenu:
                break;
            case SceneAction.MissionMap:
                GameManager.Instance.LoadMapScene();
                Cursor.visible = true;
                break;
            default:
                Debug.LogError("Scene action not recognied " + _levelToLoad.ToString());
                break;
        }

        FadeIn();
    }

    public void OnFadeInComplete()
    {
        OnFadeComplete.Invoke(true);
    }

    public void FadeIn()
    {
        if (_mainMenuAnimator == null)
        {
            return;
        }
        
        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = _fadeInAnimation;
        _mainMenuAnimator.Play();
    }

    public void FadeOut()
    {
        if (_mainMenuAnimator == null)
        {
            return;
        }
        _mainMenuAnimator.clip = _fadeOutAnimation;
        _mainMenuAnimator.Play();
    }
}
