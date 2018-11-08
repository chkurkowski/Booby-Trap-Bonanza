using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBarrel : MonoBehaviour
{

    public int rollDirection = 1;
    public float rollSpeed = 15f;
    private Rigidbody2D barrelRigidBody;
    // Use this for initialization
    void Start()
    {
        barrelRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        barrelRigidBody.velocity = new Vector3(rollSpeed * rollDirection * Time.deltaTime, barrelRigidBody.velocity.y, 0);
    }

    public void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
    }
}
