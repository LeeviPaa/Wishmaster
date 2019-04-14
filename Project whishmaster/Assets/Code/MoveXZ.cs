using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveXZ : MonoBehaviour
{
    public float speed = 100;
    public float scrollSpeed = 100;

    private void Update()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed * Time.deltaTime;
        transform.Translate(transform.forward*Input.mouseScrollDelta*scrollSpeed*Time.deltaTime, Space.World);
    }
}
