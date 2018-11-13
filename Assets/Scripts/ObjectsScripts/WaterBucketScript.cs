using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBucketScript : MonoBehaviour {
    public GameObject waterPuddle;
    public bool isActive;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isActive)
        {
            isActive = false;
            Instantiate(waterPuddle, transform.position, transform.rotation);
            //do breaking animation here
            //call Destroy at the end of the anim if it doesnt leave debris
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("Col detected");
        if (col.gameObject.tag == "Interactable" && col.gameObject.name != "WaterPuddle(Clone)")
        {
            //Debug.Log("Chandelier detected");
            isActive = true;
            
        }

    }

}
