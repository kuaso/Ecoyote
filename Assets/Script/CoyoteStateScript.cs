using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CoyoteStateScript : MonoBehaviour
{
    public Rigidbody2D coyoteRb;
    public SpriteRenderer coyoteSprite;
    public Animator coyoteAnim;
    public EndLevelScript endLevelScript;

    

    public enum AnimationState
    {
        Idle,
        Run,
        Jump,
        Fall,
        Hurt
    }

    public void UpdateAnimationState()
    {
        AnimationState state;
        var dirX = Input.GetAxisRaw("Horizontal");
        if (dirX > 0f)
        {
            state = AnimationState.Run;
            coyoteSprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = AnimationState.Run;
            coyoteSprite.flipX = true;
        }
        else if (endLevelScript.isLevelOver)
        {
            state = AnimationState.Run;
            coyoteSprite.flipX = coyoteRb.velocity.x < 0f;
        }
        else
        {
            state = AnimationState.Idle;
        }

        if (coyoteRb.velocity.y > 0.01f)
        {
            state = AnimationState.Jump;
            PlayerMovement.jumpSound.Play();

        }
        else if (coyoteRb.velocity.y < -0.1f)
        {
            state = AnimationState.Fall;
        }

        coyoteAnim.SetInteger("state", (int)state);
    }

    public void UpdateAnimationState(AnimationState state)
    {
        coyoteAnim.SetInteger("state", (int)state);
    }
}