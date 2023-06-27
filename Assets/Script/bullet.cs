using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private GameObject coyote;
    private Rigidbody2D rb;
    [SerializeField] float force;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coyote = GameObject.FindGameObjectWithTag("coyote");

        Vector3 direction = coyote.transform.position - transform.position;

        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
