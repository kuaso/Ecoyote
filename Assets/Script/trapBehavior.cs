using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapBehavior : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetBool("isTrapped", true);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        anim.SetBool("isTrapped", false);
    }
}