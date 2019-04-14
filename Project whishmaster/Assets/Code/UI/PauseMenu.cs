using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button ResumeButton;
    public Button QuitButton;
    public Button MapButton;

    private void Awake()
    {
        ResumeButton.onClick.AddListener(HandleResumeClick);
        QuitButton.onClick.AddListener(HandleQuitClick);
        MapButton.onClick.AddListener(HandleReturnToMenu);
    }

    void HandleResumeClick()
    {
        GameManager.Instance.TogglePause();
    }

    void HandleReturnToMenu()
    {
        GameManager.Instance.ReturnToMap();
    }

    void HandleQuitClick()
    {
        GameManager.Instance.QuitGame();
    }
}
