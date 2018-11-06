using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandelierRopeScript : MonoBehaviour
{
    public GameObject chandelier;
    private bool isActive;
	
    // Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isActive)
        {
            isActive = false;
            chandelier.GetComponent<ChandelierScript>().isActive = true;
            //do breaking animation here
            //call Destroy at the end of the anim if it doesnt leave debris
            Destroy(gameObject);
        }
		
	}


    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Interactable" && col.gameObject.name != "WaterPuddle(Clone)" && col.gameObject != chandelier)
        {
            isActive = true;
        }

    }
}
