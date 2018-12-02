﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScreenManagerScript : MonoBehaviour {
    public GameObject objectsButton;
    public GameObject controlsButton;
    public GameObject backButton;
    public GameObject nextScreenButton;

    [Space(20)]

    public GameObject controlPageSprite;
    public GameObject objectPageSprite;
    /// <summary>
    /// 0 is gameplay, 1 is control screen, 2 is object interaction screen.
    /// </summary>
    public int currentPausedState = 0;
	// Use this for initialization
	void Start ()
    {
        controlPageSprite.SetActive(false);
        objectPageSprite.SetActive(false);
        //
        backButton.SetActive(false);
        nextScreenButton.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		switch(currentPausedState)
        {
            case 0://play screen
                {
                    controlPageSprite.SetActive(true);
                    objectPageSprite.SetActive(true);
                    //
                    backButton.SetActive(false);
                    nextScreenButton.SetActive(false);
                    Time.timeScale = 1;
                    break;
                }
            case 1://controlPage
                {
                    controlPageSprite.SetActive(true);
                    objectPageSprite.SetActive(false);
                    //
                    backButton.SetActive(true);
                    nextScreenButton.SetActive(true);
                    Time.timeScale = 0;
                    break;
                }
            case 2://objectPage
                {
                    controlPageSprite.SetActive(false);
                    objectPageSprite.SetActive(true);
                    //
                    backButton.SetActive(true);
                    nextScreenButton.SetActive(true);
                    Time.timeScale = 0;
                    break;
                }
        }
	}
}
