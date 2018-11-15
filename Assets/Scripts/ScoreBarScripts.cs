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
            scoreBarImage.color = new Color(Random.Range(50, 155), Random.Range(50, 155), Random.Range(50, 155));
        }
        scoreBarImage.color = new Color(60, 60, 60);

    }

    public void IncreaseScoreBar()
    {
        scoreBarImage.fillAmount += increaseAmount;
    }
}
