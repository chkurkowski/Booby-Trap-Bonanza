using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameWooshScript : MonoBehaviour {
    public int directionFacing;
    public float wooshSpeed = 15f;
    public float wooshLifeTime = 1f;
    private Rigidbody2D wooshRigidBody;
	// Use this for initialization
	void Start ()
    {
        wooshRigidBody = gameObject.GetComponent<Rigidbody2D>();
        Destroy(gameObject, wooshLifeTime);
        wooshRigidBody.velocity = new Vector3(wooshSpeed * Time.deltaTime * directionFacing, 0, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
