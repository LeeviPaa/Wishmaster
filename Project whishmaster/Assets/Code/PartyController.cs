using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class E_Vector3Event : UnityEvent<Vector3> { }

public class PartyController : MonoBehaviour
{
    private NavMeshAgent _myAgent;
    private Camera _mainCam;
    private RaycastHit _hit;
    public LayerMask _layers;

    public static E_Vector3Event e_onSetDestination = new E_Vector3Event();

    public float _moveTreshold = 0.1f;
    public bool IsMoving
    {
        get
        {
            if (_myAgent)
                return _myAgent.velocity.magnitude > _moveTreshold;
            else
                return false;
        }
    }
    private bool _camping = false;
    public void ToggleCamping()
    {
        _camping = !_camping;
        _myAgent.isStopped = _camping;
    }

    private void Start()
    {
        _mainCam = Camera.main;
        _myAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            if(!_camping && !EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out _hit, _layers))
            {
                SetDestination(_hit.point);
            }
        }
    }

    public void SetDestination(Vector3 destination)
    {
        _myAgent.destination = destination;

        e_onSetDestination.Invoke(destination);
    }
}
