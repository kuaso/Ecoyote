using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingEnemy : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletPos;
    [SerializeField] float frequency;
    [SerializeField] float distance;
    [SerializeField] AudioClip shootSound;

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
        if ((timer > frequency) && (bulletPosition.x - coyotePos.x) < distance && coyotePos.x < bulletPosition.x)
        {
            timer = 0;
            shoot();
        }
    }

    void shoot() {
        sound.instance.PlaySound(shootSound);
        Instantiate(bullet, bulletPos.position, Quaternion.identity); 
    }
}
