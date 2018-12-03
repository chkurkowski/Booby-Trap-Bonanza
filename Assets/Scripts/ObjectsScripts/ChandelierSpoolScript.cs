using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandelierSpoolScript : ObjectsScript
{
    public GameObject chandelier;
    public GameObject chandelierRope;
   
    private ScoreBarScripts scoreBarInfo;
    [Range(0, 1)]
    public float scoreIncreaseAmount;
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
            if(isPossessed)
            {
                chandelier.GetComponent<ChandelierScript>().isActive = true;
                chandelier.GetComponent<ChandelierScript>().isPossessed = true;
                Destroy(chandelierRope);
            }
            else
            {
                chandelier.GetComponent<ChandelierScript>().isActive = true;
                Destroy(chandelierRope);
            }
            chandelier.GetComponent<ChandelierScript>().isActive = true;
            Destroy(chandelierRope);
            //do breaking animation here
            //call Destroy at the end of the anim if it doesnt leave debris
            Destroy(gameObject);
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
