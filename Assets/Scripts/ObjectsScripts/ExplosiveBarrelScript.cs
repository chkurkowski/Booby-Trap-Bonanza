using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrelScript : ObjectsScript
{
    public GameObject barrelExplosion;
    
    private ScoreBarScripts scoreBarInfo;
    [Range(0, 1)]
    public float scoreIncreaseAmount;
    [Space(25)]
    public int possessedExplosionsAmount = 4;
    public float xOffset;
    // Use this for initialization


    void Start()
    {
        scoreBarInfo = GameObject.Find("ScoreBar").GetComponent<ScoreBarScripts>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (isActive)
        {
            isActive = false;
            if (isPossessed)
            {
                Instantiate(barrelExplosion, transform.position, transform.rotation);
                for (int i = 0; i < possessedExplosionsAmount; i ++)
                {
                    Instantiate(barrelExplosion, new Vector3(transform.position.x + xOffset /*(float)(xOffset +i)*/, transform.position.y, transform.position.z), transform.rotation);
                    Instantiate(barrelExplosion, new Vector3(transform.position.x - xOffset/*(float)(xOffset + i)*/, transform.position.y, transform.position.z), transform.rotation);
                    Instantiate(barrelExplosion, new Vector3(transform.position.x , transform.position.y + xOffset/*(float)(xOffset + i)*/, transform.position.z), transform.rotation);
                    Instantiate(barrelExplosion, new Vector3(transform.position.x , transform.position.y - xOffset/*(float)(xOffset + i)*/, transform.position.z), transform.rotation);
                    xOffset++;
                }
            }
            else
            {
                Instantiate(barrelExplosion, transform.position, transform.rotation);
            }
            
            //do breaking animation here
            //call Destroy at the end of the anim if it doesnt leave debris
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Interactable" && col.gameObject.name != "WaterPuddle(Clone)")
        {
            isActive = true;
            scoreBarInfo.IncreaseScoreBar(scoreIncreaseAmount);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Interactable" && col.gameObject.name != "WaterPuddle(Clone)")
        {
            isActive = true;
            scoreBarInfo.IncreaseScoreBar(scoreIncreaseAmount);
        }
    }

}
