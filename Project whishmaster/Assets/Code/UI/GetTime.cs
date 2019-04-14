using UnityEngine;
using UnityEngine.UI;
using System;

public class GetTime : MonoBehaviour
{
    Text _timeText;

    private void Start()
    {
        _timeText = GetComponentInChildren<Text>();
        TimeControl.e_onTimeChange.AddListener(OnTimeChange);
    }
    private void OnDestroy()
    {
        TimeControl.e_onTimeChange.RemoveListener(OnTimeChange);
    }

    public void OnTimeChange(DateTime time)
    {
        _timeText.text = time.TimeOfDay.ToString();
    }
}
