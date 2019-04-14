using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SunTimeAngle : MonoBehaviour
{
    private void Start()
    {
        TimeControl.e_onTimeChange.AddListener(OnTimeChange);
    }
    private void OnDestroy()
    {
        TimeControl.e_onTimeChange.RemoveListener(OnTimeChange);
    }

    public void OnTimeChange(DateTime time)
    {
        TimeSpan timehour = time.TimeOfDay;
        double angle = timehour.TotalMinutes *360 / (24*60) + 180;
        transform.eulerAngles = new Vector3(0, 0, (float)angle);
    }
}
