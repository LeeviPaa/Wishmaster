using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    public float _mouseSenseX = 1;
    public float _mouseSenseY = 1;
    public Transform _headX;
    bool paused = false;

    private void Start()
    {
        Cursor.visible = false;
        GameManager.Instance.OnGameStateChanged.AddListener(OnGameState);
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnGameStateChanged.RemoveListener(OnGameState);
    }

    private void Update()
    {
        if (!Input.GetKey(KeyCode.LeftAlt) && !paused)
        {
            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * _mouseSenseY, 0) * Time.deltaTime, Space.Self);
            _headX.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * _mouseSenseX, 0, 0) * Time.deltaTime, Space.Self);
        }

        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.visible = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Cursor.visible = false;
        }
    }
    private void OnGameState(GameManager.GameState newState, GameManager.GameState oldState)
    {
        if(newState == GameManager.GameState.PAUSED)
        {
            paused = true;
            Cursor.visible = true;
        }
        if(oldState == GameManager.GameState.PAUSED)
        {
            paused = false;
            Cursor.visible = false;
        }
    }
}
