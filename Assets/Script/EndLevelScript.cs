using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelScript : MonoBehaviour
{
    public bool isLevelOver;
    public Rigidbody2D coyoteRb;
    public CamerController camScript;
    public PlayerMovement playerMovementScript;

    // Update is called once per frame
    void Update()
    {
        if (isLevelOver)
        {
            if (coyoteRb.transform.position.x < camScript.maxValues.x)
            {
                coyoteRb.velocity = new Vector2(7f, coyoteRb.velocity.y);
                playerMovementScript.UpdateAnimationState();
            }
            else
            {
                if (coyoteRb.velocity.x == 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("coyote"))
        {
            isLevelOver = true;
        }
    }
}