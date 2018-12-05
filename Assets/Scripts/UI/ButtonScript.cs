using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {
    private HelpScreenManagerScript helpManagerInfo; 


	// Use this for initialization
	void Start ()
    {
        helpManagerInfo = GameObject.Find("HelpScreenManager").GetComponent<HelpScreenManagerScript>();
	}
	


    public void ControlButton()
    {
        helpManagerInfo.currentPausedState = 1;
    }

    public void ObjectHelpButton()
    {
        helpManagerInfo.currentPausedState = 2;
    }

    public void NextScreenButton()
    {
        if(helpManagerInfo.currentPausedState == 1)
        {
            helpManagerInfo.currentPausedState = 2;
        }
        else if (helpManagerInfo.currentPausedState == 2)
        {
            helpManagerInfo.currentPausedState = 1;
        }
    }
    public void BackButton()
    {
        helpManagerInfo.currentPausedState = 0;
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("ExampleLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
