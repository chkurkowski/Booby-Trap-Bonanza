using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairScript : MonoBehaviour {

    public GameObject chairLeg;

    public ObjectsScript objectScriptInfo;
    
    
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(objectScriptInfo.isActivated)
        {
            objectScriptInfo.isActivated = true;
            Instantiate(chairLeg, transform.position, transform.rotation);
            //set animation to destroyed version
            Destroy(gameObject);
        }
	}

}
