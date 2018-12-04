using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level2Change : MonoBehaviour
{

    public GameObject triggerGoon1;
    public GameObject triggerGoon2;
    public GameObject triggerGoon3;

    public bool goonsDead = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (triggerGoon1.GetComponent<GoonAI>().state == GoonAI.State.DYING &&
           triggerGoon2.GetComponent<GoonAI>().state == GoonAI.State.DYING &&
           triggerGoon3.GetComponent<GoonAI>().state == GoonAI.State.DYING)
        {
            print("dead");
            Invoke("NextLevel", 1);
        }


    }

   private void NextLevel()
    {
        SceneManager.LoadScene("tyTestScene");
    }
}
