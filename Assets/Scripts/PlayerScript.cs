using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody playerRigidBody;

    private float h;
    private float v;
    public float speed;

    private float timeToFire = 0f;
    public float fireCooldown;
    public GameObject bulletPreFab;
    public float bulletSpeed = 10f;

    public float dodgeCoolDown = 2f;
    public float dodgeForce = 5f;
    private float timeToCanDodge;
    private Vector3 dodgeSpeed;
    public float dodgeDuration = .3f;

    

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        timeToCanDodge = Time.time;
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        transform.Translate(h * speed * Time.deltaTime, 0, v > 0 ? transform.position.z <= 1 ? v * speed * Time.deltaTime : 0 : v * speed * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Time.time >= timeToFire)
            {
                GameObject bullet = Instantiate(bulletPreFab, transform.position, transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = Vector3.forward * bulletSpeed;
                timeToFire = Time.time + fireCooldown;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(Time.time >= timeToCanDodge)
            {
                if (Input.GetKey(KeyCode.D))
                {
                    StartCoroutine(Dodge(new Vector3(dodgeForce * Time.deltaTime, 0, 0)));
                    timeToCanDodge = Time.time + dodgeCoolDown;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    StartCoroutine(Dodge(new Vector3(-dodgeForce * Time.deltaTime, 0, 0)));
                    timeToCanDodge = Time.time + dodgeCoolDown;
                }
            }
        }
        transform.Translate(dodgeSpeed);
    }

    private IEnumerator Dodge(Vector3 force)
    {
        dodgeSpeed = force;
        yield return new WaitForSeconds(.1f);
        dodgeSpeed = Vector3.zero;
    }
}
