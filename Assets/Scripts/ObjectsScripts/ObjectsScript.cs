using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsScript : MonoBehaviour {

    public bool isActivated;
  
    public GameObject chairLeg;
    public GameObject chandelier;
    public GameObject barrelExplosion;
    public GameObject flameWoosh;
    public int wooshAmounts;
    public GameObject waterPuddle;
    
  


	
	// Update is called once per frame
	void Update ()
    {
		if(isActivated)
        {
            isActivated = false;
            if(gameObject.tag == "Chair")
            {
                Instantiate(chairLeg, transform.position, transform.rotation);
                //do breaking animation here
                //call Destroy at the end of the anim if it doesnt leave debris
                Destroy(gameObject);
            }
            else if(gameObject.tag == "ChandelierRope")
            {
                chandelier.GetComponent<ChandelierScript>().isActive = true;
                //do breaking animation here
                //call Destroy at the end of the anim if it doesnt leave debris
                Destroy(gameObject);
            }
            else if(gameObject.tag == "Fireplace")
            {
                
                for(int i = 0; i < wooshAmounts; i++)
                {
                    //spawnDirection *= -1;
                   // Instantiate(flameWoosh, transform.position + new Vector3(i * spawnDirection, transform.position.y, transform.position.z), transform.rotation);
                }
               
                //do breaking animation here
                //call Destroy at the end of the anim if it doesnt leave debris

            }
            else if(gameObject.tag == "ExplosiveBarrel")
            {
                Instantiate(barrelExplosion, transform.position, transform.rotation);
                //do breaking animation here
                //call Destroy at the end of the anim if it doesnt leave debris
                Destroy(gameObject);
            }
            else if(gameObject.tag == "WaterBucket")
            {
                Instantiate(waterPuddle, transform.position, transform.rotation);
                //do breaking animation here
                //call Destroy at the end of the anim if it doesnt leave debris
                Destroy(gameObject);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if(col.gameObject.tag == "Interactable" && col.gameObject.name != "WaterPuddle(Clone)" )
        {
            isActivated = true;
        }

    }
}
