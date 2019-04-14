using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMenu : MonoBehaviour
{
    public Button ExpeditionButton;

    private void Awake()
    {
        ExpeditionButton.onClick.AddListener(HandleExpeditionClick);
    }

    void HandleExpeditionClick()
    {
        UIManager.Instance.FadeIn(SceneAction.Expedition);
    }
}
