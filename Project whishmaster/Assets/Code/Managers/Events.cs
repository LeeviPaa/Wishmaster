using UnityEngine;
using UnityEngine.Events;

public class Events
{
    [System.Serializable] public class EventFadeComplete : UnityEvent<bool> { }
    [System.Serializable] public class EventVoid : UnityEvent<bool> { }
    [System.Serializable] public class EventFloat : UnityEvent<float> { }
}