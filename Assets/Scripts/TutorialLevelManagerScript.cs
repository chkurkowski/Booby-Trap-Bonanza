using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialLevelManagerScript : MonoBehaviour {

    public GoonAI triggerGoon;
    private bool flag = false;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!triggerGoon.alive && !flag)
        {
            flag = true;
            Invoke("NextLevel", 1);
        }
	}

    private void NextLevel()
    {
        SceneManager.LoadScene("ExampleLevel");
    }
}
