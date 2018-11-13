using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialLevelManagerScript : MonoBehaviour {
    public GameObject triggerGoon;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (triggerGoon == null)
        {
            SceneManager.LoadScene("ExampleLevel");
        }
	}
}
