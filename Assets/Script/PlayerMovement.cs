using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _coyoteRb;
    private BoxCollider2D _coll;

    [SerializeField] private LayerMask jumpAbility;
    [SerializeField] public float maxMoveSpeed;
    [SerializeField] private float jumpForce;

    private float _rightVelocity;
    private float _leftVelocity;

    public HealthManagerScript healthManager;
    public EndLevelScript endLevelScript;
    public CoyoteStateScript coyoteStateScript;

    // Start is called before the first frame update
    void Start()
    {
        _coyoteRb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseScript.IsPaused || healthManager.hasDied || endLevelScript.isLevelOver) return;
        var x = 0f;

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) &&
            CanJump())
        {
            _coyoteRb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (_rightVelocity <= maxMoveSpeed)
            {
                _rightVelocity += 0.5f;
            }
            else
            {
                _rightVelocity = maxMoveSpeed;
            }

            x += _rightVelocity;
        }
        else
        {
            _rightVelocity = 0f;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (_leftVelocity <= maxMoveSpeed)
            {
                _leftVelocity += 0.5f;
            }
            else
            {
                _leftVelocity = maxMoveSpeed;
            }

            x -= _leftVelocity;
        }
        else
        {
            _leftVelocity = 0f;
        }

        if (x == 0)
        {
            // Take the current velocity and take 95% of its x value to slow down
            var alteredXVelocity = (_coyoteRb.velocity.x * 0.95f);
            if (Math.Round(alteredXVelocity, 10) != 0) x = alteredXVelocity;
        }

        _coyoteRb.velocity = new Vector2(x, _coyoteRb.velocity.y);

        coyoteStateScript.UpdateAnimationState();
    }


    private bool CanJump() =>
        Physics2D.BoxCast(_coll.bounds.center, _coll.bounds.size, 0f, Vector2.down, 0.1f, jumpAbility);
}