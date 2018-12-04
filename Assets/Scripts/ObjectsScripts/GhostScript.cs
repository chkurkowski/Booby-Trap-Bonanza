using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour {

    public GameObject target;
    public GameObject particles;
    private float speed = 5f;

    private float timer = 0;
    private const float STARTDELAY = 1.5f;
	
	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;



        if(timer >= STARTDELAY)
        {
            RotateToTarget();
            MoveToTarget();
        }
	}

    private void RotateToTarget()
    {
        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();

        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
    }

    private void MoveToTarget()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void SetTarget(GameObject gm)
    {
        target = gm;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject == target)
        {
            target.GetComponent<ObjectsScript>().isPossessed = true;
            target.GetComponent<ObjectsScript>().isActive = true;
            Destroy(particles);
            Destroy(gameObject);
        }
    }
}
