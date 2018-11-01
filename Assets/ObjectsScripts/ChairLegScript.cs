using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairLegScript : MonoBehaviour {
    public float chairLegSpeed;
    private Rigidbody2D chairLegRigidBody;
	// Use this for initialization
	void Start ()
    {
        chairLegRigidBody = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        chairLegRigidBody.velocity = Vector3.left * chairLegSpeed * Time.deltaTime;
	}
}
