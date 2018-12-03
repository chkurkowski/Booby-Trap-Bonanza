using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireplaceScript : ObjectsScript
{
    public GameObject flameWoosh;
    public float wooshSpawnRate = .25f;
    public float wooshSpawnTime = 1f;
    private ScoreBarScripts scoreBarInfo;
    [Range(0, 1)]
    public float scoreIncreaseAmount;
    // Use this for initialization


    void Start()
    {
        scoreBarInfo = GameObject.Find("ScoreBar").GetComponent<ScoreBarScripts>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            isActive = false;
            if(isPossessed)
            {
                wooshSpawnRate /= 2;
            }
            InvokeRepeating("SpawnWoosh", 0, wooshSpawnRate);
            Invoke("CancelSpawnWoosh", wooshSpawnTime);
        }
	}

    public void SpawnWoosh()
    {
     GameObject wooshRight = Instantiate(flameWoosh, transform.position/*new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z)*/, transform.rotation);
        wooshRight.GetComponent<FlameWooshScript>().directionFacing = 1;
        
     GameObject wooshLeft = Instantiate(flameWoosh, transform.position/*new Vector3(transform.position.x + (xOffset * -1), transform.position.y, transform.position.z)*/, transform.rotation);
        wooshLeft.GetComponent<FlameWooshScript>().directionFacing = -1;
        wooshLeft.transform.localScale = new Vector3(wooshLeft.transform.localScale.x * -1, wooshLeft.transform.localScale.y, wooshLeft.transform.localScale.z);
        if (isPossessed)
        {
            wooshRight.GetComponent<FlameWooshScript>().wooshSpeed *= 5;
            wooshLeft.GetComponent<FlameWooshScript>().wooshSpeed *= 5;
        }
        //xOffset += xOffsetAmount;  
    }
    public void CancelSpawnWoosh()
    {
        CancelInvoke("SpawnWoosh");
        //This method literally exists because CancelInvoke doesn't contain a delay parameter
        //Also because I didn't want to do a coroutine because fuck coroutines
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Interactable" && col.gameObject.name != "WaterPuddle(Clone)" && col.gameObject.name != "FlameWoosh(Clone)")
        {
            isActive = true;
            scoreBarInfo.IncreaseScoreBar(scoreIncreaseAmount);
        }
    }
}
