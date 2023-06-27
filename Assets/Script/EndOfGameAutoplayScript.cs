using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGameAutoplayScript : MonoBehaviour
{
    private Rigidbody2D _coyoteRb;
    public CoyoteStateScript stateScript;
    public CamerController camScript;
    private int _countedFrames = 0;
    private const int MaxFramesTillReset = 300;

    // Start is called before the first frame update
    void Start()
    {
        _coyoteRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_coyoteRb.position.x >= camScript.maxValues.x)
        {
            stateScript.UpdateAnimationState(CoyoteStateScript.AnimationState.Idle);
            ++_countedFrames;
            if (_countedFrames >= MaxFramesTillReset)
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            _coyoteRb.velocity = Vector2.right * 7;
            stateScript.UpdateAnimationState(CoyoteStateScript.AnimationState.Run);
        }
    }
}