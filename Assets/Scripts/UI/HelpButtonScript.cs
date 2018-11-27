using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButtonScript : MonoBehaviour {
    private HelpScreenManagerScript helpManagerInfo; 
	// Use this for initialization
	void Start ()
    {
        helpManagerInfo = GameObject.Find("HelpScreenManager").GetComponent<HelpScreenManagerScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
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
}
