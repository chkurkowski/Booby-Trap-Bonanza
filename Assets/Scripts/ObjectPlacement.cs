using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour {

    public GameObject explodingBarrel;
    public GameObject waterBarrel;
    public GameObject rollingBarrel;
    public Camera cam;

    /*
     * 1 = explodingBarrel
     * 2 = waterBarrel
     * 3 = rollingBarrel
     */
    [SerializeField]
    private int selectedObject;
    [SerializeField]
    private Vector3 mousePosition;
    [SerializeField]
    private GameObject currentObject;
    private bool validPos = false;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        GetMousePosition();
        FollowMouse();
        ClickToPlace();

        TempObjectSelect();

        if (selectedObject != 0)
            SelectItem();
	}

    private void TempObjectSelect()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            selectedObject = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedObject = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedObject = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedObject = 3;
        }
    }

    private void GetMousePosition()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = -5;
    }

    private void SelectItem()
    {
        if (selectedObject == 1)
        {
            currentObject = explodingBarrel;
        }
        else if (selectedObject == 2)
        {
            currentObject = waterBarrel;
        }
        else if (selectedObject == 3)
        {
            currentObject = rollingBarrel;
        }
        else
        {
            currentObject = null;
        }
    }

    private void FollowMouse()
    {
        if(currentObject != null)
        {
            currentObject.transform.position = mousePosition;
            CheckValidPosition();
        }
    }

    private void CheckValidPosition()
    {
        if(Physics2D.OverlapBox(mousePosition, new Vector2(1, 1), 0f) != null)
        {
            //Set sprite to red
            currentObject.GetComponent<SpriteRenderer>().color = Color.red;
            validPos = false;
        }
        else 
        {
            //Set sprite to green
            currentObject.GetComponent<SpriteRenderer>().color = Color.green;
            validPos = true;
        }
    }

    private void ClickToPlace()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && currentObject != null && validPos)
        {

        }
    }
}
