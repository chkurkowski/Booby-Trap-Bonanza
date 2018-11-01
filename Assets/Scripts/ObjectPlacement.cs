using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour {

    public GameObject explodingBarrel;
    public GameObject waterBarrel;
    public GameObject rollingBarrel;
    public Camera cam;

    /*
     * 0 = explodingBarrel
     * 1 = waterBarrel
     * 2 = rollingBarrel
     */
    private int selectedObject;
    private Vector3 mousePosition;

	// Use this for initialization
	void Start () 
    {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        GetMousePosition();

        if (selectedObject == 0)
        {
            GetMousePosition();
        }
        else if (selectedObject == 1)
        {
            GetMousePosition();
        }
        else if(selectedObject == 2)
        {
            GetMousePosition();
        }
	}

    private void GetMousePosition()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = -5;
    }

    private void FollowMouse(GameObject obj)
    {
        if (obj == null)
        {
            return;
        }


    }
}
