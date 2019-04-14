using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationMarker : MonoBehaviour
{
    public GameObject _visuals;

    private void Start()
    {
        foreach(Transform child in transform)
        {
            if (child.name == "visuals")
            {
                _visuals = child.gameObject;
                _visuals.SetActive(false);
            }
        }
        PartyController.e_onSetDestination.AddListener(OnSetDestination);
    }
    private void OnDestroy()
    {
        PartyController.e_onSetDestination.RemoveListener(OnSetDestination);
    }

    private void OnSetDestination(Vector3 destination)
    {
        _visuals.SetActive(true);
        transform.position = destination;
        transform.Translate(new Vector3(0, 10, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PartyController>())
        {
            _visuals.SetActive(false);
        }
    }
}
