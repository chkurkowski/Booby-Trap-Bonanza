using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostJarScript : ObjectsScript {

    public GameObject target;
    public GameObject ghost;
    private ScoreBarScripts scoreBarInfo;
    private ObjectPlacement placementManager;
    [Range(0, 1)]
    public float scoreIncreaseAmount = 0.2f;

    void Start()
    {
        scoreBarInfo = GameObject.Find("ScoreBar").GetComponent<ScoreBarScripts>();
        placementManager = ObjectPlacement.inst;
    }

    // Update is called once per frame
    private void Update () 
    {
        BreakJar();
    }

    private void BreakJar()
    {
        if (isActive)
        {
            GameObject ghostGM = Instantiate(ghost, transform.position + Vector3.up, Quaternion.identity);
            ghostGM.GetComponent<GhostScript>().target = target;
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
