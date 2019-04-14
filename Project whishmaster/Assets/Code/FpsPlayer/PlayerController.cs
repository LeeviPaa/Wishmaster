using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    public Animator _animator;
    public float _speed = 5;
    public float _runMultiplier = 1.5f;

    private float _gravityV = 0;
    public float _gravityAcceleration = 9.81f;
    public float _terminalVelcoty = 100;

    private bool _jumping = false;
    public float _jumpForce = 100;
    public float _highJumpTime = 0.1f;
    public LayerMask _jumpLayer;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Jumping();
        Gravity();
        Movement();
    }

    Coroutine _highJump;
    private void Jumping()
    {
        if (GroundBeneath(0.1f) && Input.GetButtonDown("Jump"))
        {
            _gravityV = _jumpForce;
            _jumping = true;
            _highJump = StartCoroutine(HighJump());
        }
        else
            _jumping = false;
    }

    private IEnumerator HighJump()
    {
        float t = 0;
        while (t < _highJumpTime && Input.GetButton("Jump"))
        {
            _gravityV = _jumpForce;
            _jumping = true;
            yield return null;
            t += Time.deltaTime;
        }
    }

    private void Movement()
    {
        Vector3 movement = (transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical")) * _speed * Time.deltaTime;
        if (Input.GetButton("Run"))
            movement *= _runMultiplier;

        _controller.Move(movement);
        _animator.SetFloat("speed", movement.magnitude);

        if (movement.magnitude < 0.001f)
            _animator.SetBool("moving", false);
        else
            _animator.SetBool("moving", true);
    }

    private void Gravity()
    {
        if (_controller.isGrounded && !_jumping)
        {
            _gravityV = 0;
            return;
        }

        _gravityV -= _gravityAcceleration * Time.deltaTime;
        if ( _gravityV > _terminalVelcoty)
            _gravityV = _terminalVelcoty;
        if (_gravityV < -_terminalVelcoty)
            _gravityV = -_terminalVelcoty;

        _controller.Move(transform.up * _gravityV * Time.deltaTime);
    }

    private bool GroundBeneath(float dist)
    {
        return Physics.Raycast(transform.position, -Vector3.up, dist, _jumpLayer);
    }
}
