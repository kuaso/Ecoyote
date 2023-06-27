using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingEnemy : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletPos;
    [SerializeField] float frequency;

    private float timer;
    public GameObject coyote;

    private Vector2 bulletPosition;
    private Vector2 coyotePos;

    // Start is called before the first frame update
    void Start()
    {
         //this is messed up naming covention ik
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 coyotePos = coyote.transform.position;
        Vector2 bulletPosition = bulletPos.position;
        timer += Time.deltaTime;
        if ((timer > frequency) && coyotePos.x < bulletPosition.x)
        {
            
            timer = 0;
            shoot();
        }
    }

    void shoot() {
        Instantiate(bullet, bulletPos.position, Quaternion.identity); 
    }

    public static float AngleDir(Vector2 A, Vector2 B)
    {
        //returns a negative number if B is left of 
        //positive if right of A
        //or 0 if they are perfectly aligned

        return -A.x * B.y + A.y * B.x;
    }
}
