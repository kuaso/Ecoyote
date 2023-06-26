using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _coyoteRb;
    private Animator _coyoteAnim;
    private SpriteRenderer _coyoteSprite;
    private BoxCollider2D _coll;

    [SerializeField] private LayerMask jumpAbility;
    [SerializeField] public float maxMoveSpeed;
    [SerializeField] public float jumpForce;
    private float _rightVelocity;
    private float _leftVelocity;

    private enum MoveState
    {
        Idle,
        Run,
        Jump,
        Fall
    }

    // Start is called before the first frame update
    void Start()
    {
        _coyoteAnim = GetComponent<Animator>();
        _coll = GetComponent<BoxCollider2D>();
        _coyoteRb = GetComponent<Rigidbody2D>();
        _coyoteSprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (PauseScript.IsPaused) return;
        var x = 0f;

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && CanJump())
        {
            _coyoteRb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (_rightVelocity == 0f)
            {
                _rightVelocity = 0.5f;
            }
            else if (_rightVelocity <= maxMoveSpeed)
            {
                _rightVelocity *= 1.1f;
            }
            
            if (_rightVelocity > maxMoveSpeed)
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
            if (_leftVelocity == 0f)
            {
                _leftVelocity = 0.5f;
            }
            else if (_leftVelocity <= maxMoveSpeed)
            {
                _leftVelocity *= 1.1f;
            }
            
            if (_leftVelocity > maxMoveSpeed)
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

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MoveState state;
        float dirX = Input.GetAxisRaw("Horizontal");
        if (dirX > 0f) {
            state = MoveState.Run;
            _coyoteSprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MoveState.Run;
            _coyoteSprite.flipX = true;
        }
        else
        {
            state = MoveState.Idle;
        }

        if (_coyoteRb.velocity.y > 0.01f) {
            state = MoveState.Jump;
        }
        else if (_coyoteRb.velocity.y < -0.1f)
        {
            state = MoveState.Fall;
        }

        _coyoteAnim.SetInteger("state", (int)state);
    }

    private bool CanJump() => Physics2D.BoxCast(_coll.bounds.center, _coll.bounds.size, 0f, Vector2.down, 0.1f, jumpAbility);
    
}