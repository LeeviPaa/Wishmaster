using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToggleButton : MonoBehaviour
{
    private bool toggleState = false;
    private Button _thisButton;
    private ColorBlock _buttonColors;
    private ColorBlock _toggledColors;

    private void Start()
    {
        _thisButton =  GetComponent<Button>();
        _buttonColors = _thisButton.colors;
        _toggledColors = _buttonColors;
        _toggledColors.normalColor = Color.green;
        _toggledColors.highlightedColor = Color.green;
    }

    public void ToggleButtonState()
    {
        _thisButton = _thisButton ?? GetComponent<Button>();
        toggleState = !toggleState;

        if (toggleState)
            _thisButton.colors = _toggledColors;
        else
            _thisButton.colors = _buttonColors;

    }
}
