using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandelierScript : MonoBehaviour {
    public bool isActive;
    public bool isFalling= false;
    public bool isPossessed;
    private Rigidbody2D chandelierRigidBody;
    public float colliderDelay = 1;
   
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
            isFalling = true;
            chandelierRigidBody.constraints = RigidbodyConstraints2D.None;
            if(isPossessed)
            {
                gameObject.GetComponent<Collider2D>().enabled = false;
                transform.localScale = new Vector3(2, 2, 1);
                Invoke("TurnCollidersOn", colliderDelay);


            }
        }
	}
    public void TurnCollidersOn()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    /* private void OnCollisionEnter2D(Collision2D collision)
     {
        // if(isActive)
         //{
         if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground")
         {
             Destroy(gameObject);
         }

             //if we need code here for spawning killBoxes or something
         //}
     }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
         if(isFalling)
        {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }

        //if we need code here for spawning killBoxes or something
        }
    }
}
