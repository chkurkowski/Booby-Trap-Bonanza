﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsScript : MonoBehaviour {

    public bool isActivated;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        
        if(col.gameObject.tag == "Interactable")
        {
            
        }

    }
}
