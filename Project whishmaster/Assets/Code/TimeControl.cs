using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class E_DateTimeEvent : UnityEvent<DateTime> { }

public class TimeControl : MonoBehaviour
{
    public float _timeScale = 100;
    public GameObject _theSun;
    public PartyController _theParty;
    [SerializeField]
    public DateTime _timeOfDay;
    public bool _timeflow = false;

    public void ToggleTimeflow()
    {
        _timeflow = !_timeflow;
    }

    public static E_DateTimeEvent e_onTimeChange;

    private void Awake()
    {
        e_onTimeChange = new E_DateTimeEvent();

        _timeOfDay = _timeOfDay.AddYears(2033);
        _timeOfDay = _timeOfDay.AddMonths(5);
        _timeOfDay = _timeOfDay.AddDays(24);
        _timeOfDay = _timeOfDay.AddHours(12);
        print("Start date: "+_timeOfDay.Date);
        print("Start time: "+_timeOfDay.TimeOfDay);
    }

    private void Update()
    {
        if(_theParty && _theParty.IsMoving || _timeflow)
        {
            double deltaTime = _timeScale * Time.deltaTime;
            _timeOfDay = _timeOfDay.AddMinutes(deltaTime);
            e_onTimeChange.Invoke(_timeOfDay);
        }
    }
}
