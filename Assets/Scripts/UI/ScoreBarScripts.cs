using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBarScripts : MonoBehaviour
{
    public Image scoreBarImage;
    public Image skullsImage;
    private Color[] barColors = new Color[5];
    private int colorIterator = 1;
    public float subParPenalty = .1f;
    


	// Use this for initialization
	void Start ()
    {
        barColors[0] = Color.blue;
        barColors[1] = Color.green;
        barColors[2] = Color.yellow;
        barColors[3] = Color.red;
        barColors[4] = Color.magenta;
        scoreBarImage = gameObject.GetComponent<Image>();
        skullsImage = GameObject.Find("Skulls").GetComponent<Image>();
        skullsImage.fillAmount = 0;
        scoreBarImage.color = barColors[0];
        skullsImage.color = barColors[0];
	}
	
	// Update is called once per frame
	void Update ()
    {
        ColorChecker();
        if (scoreBarImage.fillAmount >= 1)
        {
          
            scoreBarImage.fillAmount = 0;
            if(skullsImage.fillAmount < 1)
            {
                skullsImage.fillAmount += .2f;
                if(skullsImage.fillAmount > 1)
                {
                    skullsImage.fillAmount = 1;
                }
            }
        }
        
        //Debug.Log("ColorChange should have happened.");

    }

    public void ColorChecker()
    {
        if(skullsImage.fillAmount <= .2)
        {
           // skullsImage.color = barColors[0];
            scoreBarImage.color = barColors[0];
        }
        else if(skullsImage.fillAmount <= .4)
        {
            skullsImage.color = barColors[0];
            scoreBarImage.color = barColors[1];
        }
        else if(skullsImage.fillAmount <= .6)
        {
            skullsImage.color = barColors[1];
            scoreBarImage.color = barColors[2];
        }
        else if (skullsImage.fillAmount <= .8)
        {
            skullsImage.color = barColors[2];
            scoreBarImage.color = barColors[3];
        }
        else if (skullsImage.fillAmount < 1)
        {
            skullsImage.color = barColors[3];
            scoreBarImage.color = barColors[4];
        }
        else if(skullsImage.fillAmount >= 1)
        {
            skullsImage.color = barColors[4];
            scoreBarImage.color = barColors[4];
        }
        else
        {
            skullsImage.color = barColors[0];
            scoreBarImage.color = barColors[0];
        }
    }

    public void IncreaseScoreBar(float scoreBarIncreaseAmount)
    {
        scoreBarImage.fillAmount += scoreBarIncreaseAmount;
    }

    public void DecreaseMaximumScore()
    {
        skullsImage.fillAmount -= subParPenalty;
        
    }
}
