using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {
    public float growthRate;
    public float burnLength;
    // Use this for initialization
    void Start ()
    {
        Invoke("Destroy", burnLength);
        InvokeRepeating("Grow", 0, .01f);
    }

    public void Destroy()
    {
        CancelInvoke("Grow");
        Destroy(gameObject);
    }

    public void Grow()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + growthRate, gameObject.transform.localScale.y + growthRate, gameObject.transform.localScale.z);
    }
}
