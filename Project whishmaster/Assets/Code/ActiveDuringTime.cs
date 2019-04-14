using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActiveDuringTime : MonoBehaviour
{
    public GameObject _activeObject;
    public int _activeStartHour = 20;
    public int _activeStopHour = 5;

    public void Start()
    {
        TimeControl.e_onTimeChange.AddListener(OntTimeChange);
    }
    public void OnDestroy()
    {
        TimeControl.e_onTimeChange.RemoveListener(OntTimeChange);
    }

    public void OntTimeChange(DateTime time)
    {
        _activeObject.SetActive(time.Hour >= _activeStartHour || time.Hour <= _activeStopHour);
    }
}
