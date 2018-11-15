using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBarScripts : MonoBehaviour
{
    public Image scoreBarImage;

    [Range(0,1)]
    public float increaseAmount;
	// Use this for initialization
	void Start ()
    {
        scoreBarImage = gameObject.GetComponent<Image>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(scoreBarImage.fillAmount >= 1)
        {
            scoreBarImage.fillAmount = 0;
            scoreBarImage.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
        
        Debug.Log("ColorChange should have happened.");

    }

    public void IncreaseScoreBar()
    {
        scoreBarImage.fillAmount += increaseAmount;
    }
}
