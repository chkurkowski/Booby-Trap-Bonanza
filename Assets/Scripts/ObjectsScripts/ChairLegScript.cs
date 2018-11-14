using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairLegScript : MonoBehaviour {
    public float chairLegSpeed;
    public float direction;
    private Rigidbody2D chairLegRigidBody;
	// Use this for initialization
	void Start ()
    {
        chairLegRigidBody = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (direction < 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        else
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        chairLegRigidBody.velocity = Vector3.left * chairLegSpeed * direction * Time.deltaTime;
	}

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
    }

  

}
