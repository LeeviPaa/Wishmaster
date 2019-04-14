using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeControl : MonoBehaviour
{
    public MainMenu _mainMenu;
    private void Start()
    {
        _mainMenu = _mainMenu ?? FindObjectOfType<MainMenu>();
    }
    public void OnFadeInComplete()
    {
        _mainMenu.OnFadeInComplete();
    }
    public void OnFadeOutComplete()
    {
        _mainMenu.OnFadeOutComplete();
    }
}
