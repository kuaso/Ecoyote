using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D coyoteRigidbody;
    private bool _facingRight = true;
    private float _rightVelocity = 0f;
    private float _leftVelocity = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var x = 0f;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            coyoteRigidbody.velocity = Vector2.up * 6.5f;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (_rightVelocity == 0f)
            {
                _rightVelocity = 0.5f;
            }
            else if (_rightVelocity < 5f)
            {
                _rightVelocity *= 1.1f;
            }
            else
            {
                _rightVelocity = 5f;
            }

            x += _rightVelocity;
        }
        else
        {
            _rightVelocity = 0f;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (_leftVelocity == 0f)
            {
                _leftVelocity = 0.5f;
            }
            else if (_leftVelocity < 5f)
            {
                _leftVelocity *= 1.1f;
            }
            else
            {
                _leftVelocity = 5f;
            }

            x -= _leftVelocity;
        }
        else
        {
            _leftVelocity = 0f;
        }

        if (x == 0)
        {
            // Take the current velocity and take 90% of its x value to slow down
            var alteredXVelocity = (coyoteRigidbody.velocity.x * 0.95f);
            if (Math.Round(alteredXVelocity, 10) != 0) x = alteredXVelocity;
        }

        if (x >= 0 && !_facingRight) FlipCharacter();
        else if (x < 0 && _facingRight) FlipCharacter();

        coyoteRigidbody.velocity = new Vector2(x, coyoteRigidbody.velocity.y);
    }

    void FlipCharacter()
    {
        var currentScale = coyoteRigidbody.transform.localScale;
        currentScale.x *= -1;
        coyoteRigidbody.transform.localScale = currentScale;

        _facingRight = !_facingRight;
    }
}