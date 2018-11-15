using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandelierScript : MonoBehaviour {
    public bool isActive;
    private Rigidbody2D chandelierRigidBody;
   
    // Use this for initialization


    void Start()
    {
       
        chandelierRigidBody = gameObject.GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void Update () {
		if(isActive)
        {
            isActive = false;
            chandelierRigidBody.constraints = RigidbodyConstraints2D.None;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
       // if(isActive)
        //{
            Destroy(gameObject);
            //if we need code here for spawning killBoxes or something
        //}
    }
}
