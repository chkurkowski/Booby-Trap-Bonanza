using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScreenManagerScript : MonoBehaviour {
    public GameObject objectInteractionHelpButton;
    public GameObject controlsHelpButton;
    public GameObject backButton;
    public GameObject nextScreenButton;

    [Space(20)]

    public Sprite controlPageSprite;
    public Sprite objectPageSprite;
    /// <summary>
    /// 0 is gameplay, 1 is control screen, 2 is object interaction screen.
    /// </summary>
    public int currentPausedState = 0;
	// Use this for initialization
	void Start ()
    {
        backButton.SetActive(false);
        nextScreenButton.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		switch(currentPausedState)
        {
            case 0:
                {
                    Time.timeScale = 1;
                    break;
                }
            case 1:
                {
                    Time.timeScale = 0;
                    break;
                }
            case 2:
                {
                    Time.timeScale = 0;
                    break;
                }
        }
	}
}
