using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandelierSpoolScript : MonoBehaviour {
    public GameObject chandelier;
    public GameObject chandelierRope;
    private bool isActive;
    private ScoreBarScripts scoreBarInfo;
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
            scoreBarInfo.IncreaseScoreBar();
        }

    }
}
