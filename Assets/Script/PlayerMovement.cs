using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D coyoteRb;
    private Animator coyoteAnim;
    private SpriteRenderer coyoteSprite;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask jumpability;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 6.5f;
    private float _rightVelocity = 0f;
    private float _leftVelocity = 0f;

    private enum moveState
    {
        idle,
        run,
        jump,
        fall
    }

    private moveState state = moveState.idle;

    // Start is called before the first frame update
    void Start()
    {
        coyoteAnim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        coyoteRb = GetComponent<Rigidbody2D>();
        coyoteSprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (PauseScript.IsPaused) return;
        var x = 0f;

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && isGrounded())
        {
            // TODO something to prevent double jumps
            coyoteRb.velocity = Vector2.up * jumpForce;
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
                _rightVelocity = moveSpeed;
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
                _leftVelocity = moveSpeed;
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
            var alteredXVelocity = (coyoteRb.velocity.x * 0.95f);
            if (Math.Round(alteredXVelocity, 10) != 0) x = alteredXVelocity;
        }

        coyoteRb.velocity = new Vector2(x, coyoteRb.velocity.y);

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        moveState state;
        float dirX = Input.GetAxisRaw("Horizontal");
        if (dirX > 0f) {
            state = moveState.run;
            coyoteSprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = moveState.run;
            coyoteSprite.flipX = true;
        }
        else
        {
            state = moveState.idle;
        }

        if (coyoteRb.velocity.y > 0.01f) {
            state = moveState.jump;
        }
        else if (coyoteRb.velocity.y < -0.1f)
        {
            state = moveState.fall;
        }

        coyoteAnim.SetInteger("state", (int)state);
    }

    private bool isGrounded() {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpability);

    }
}