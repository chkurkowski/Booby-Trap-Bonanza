﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBucketScript : MonoBehaviour {
    public GameObject waterPuddle;
    public bool isActive;
    private ScoreBarScripts scoreBarInfo;
    [Range(0, 1)]
    public float scoreIncreaseAmount;
    // Use this for initialization


    void Start()
    {
        scoreBarInfo = GameObject.Find("ScoreBar").GetComponent<ScoreBarScripts>();
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
            scoreBarInfo.IncreaseScoreBar(scoreIncreaseAmount);

        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Col detected");
        if (col.gameObject.tag == "Interactable" && col.gameObject.name != "WaterPuddle(Clone)")
        {
            //Debug.Log("Chandelier detected");
            isActive = true;
            scoreBarInfo.IncreaseScoreBar(scoreIncreaseAmount);

        }

    }

}
