using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementScript : MonoBehaviour
{

    public Rigidbody2D coyoteRigidbody;

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
            coyoteRigidbody.velocity = Vector2.up * 20;
        }
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            x -= 10;
        }
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            x += 10;
        }
        
        if (x == 0)
        {
            // Take the current velocity and take 90% of its x value to slow down
            var alteredXVelocity = (coyoteRigidbody.velocity.x * 0.85f);
            if (Math.Round(alteredXVelocity, 5) != 0) x = alteredXVelocity; 
        }

        coyoteRigidbody.velocity = new Vector2(x, coyoteRigidbody.velocity.y);
    }
}
