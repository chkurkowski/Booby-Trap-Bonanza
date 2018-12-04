using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpScreenManagerScript : MonoBehaviour {
    
    public GameObject objectsButton;
    public GameObject controlsButton;
    public GameObject resumeButton;
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
        
        if(SceneManager.GetActiveScene().name == "HelpScene")
        {
            Debug.Log("ehh");
            currentPausedState = 1;
        }
        else
        {
            currentPausedState = 0;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		switch(currentPausedState)
        {
            case 0://play screen
                {
                   
                        controlPageSprite.SetActive(false);
                        objectPageSprite.SetActive(false);


                    //
                    if (SceneManager.GetActiveScene().name != "HelpScene")
                    {
                        objectsButton.SetActive(true);
                        controlsButton.SetActive(true);
                    }
                    resumeButton.SetActive(false);
                    nextScreenButton.SetActive(false);
                   
                    Time.timeScale = 1;
                    break;
                }
            case 1://controlPage
                {
                    controlPageSprite.SetActive(true);
                    objectPageSprite.SetActive(false);
                    //
                    resumeButton.SetActive(true);
                    nextScreenButton.SetActive(true);
                    if (SceneManager.GetActiveScene().name != "HelpScene")
                    {
                        objectsButton.SetActive(false);
                        controlsButton.SetActive(false);
                        Time.timeScale = 0;
                    }
                   
                    break;
                }
            case 2://objectPage
                {
                    controlPageSprite.SetActive(false);
                    objectPageSprite.SetActive(true);
                    //
                    resumeButton.SetActive(true);
                    nextScreenButton.SetActive(true);
                    if (SceneManager.GetActiveScene().name != "HelpScene")
                    {
                        objectsButton.SetActive(false);
                        controlsButton.SetActive(false);
                        Time.timeScale = 0;
                    }
                    
                    break;
                }
        }
	}
}
