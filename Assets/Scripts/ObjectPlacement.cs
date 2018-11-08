using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour {

    public GameObject explodingBarrelPreview;
    public GameObject waterBarrelPreview;
    public GameObject rollingBarrelPreview;
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
    private Vector2 objectSize;

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

	}

    private void GetMousePosition()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = -5;
    }

    public void SelectItem(int s)
    {
        if (selectedObject != 0)
            Destroy(currentObject);
       
        selectedObject = s;

        if (selectedObject == 1)
        {
            GameObject gm = Instantiate(explodingBarrelPreview);
            currentObject = gm;
            objectSize = new Vector2(1.5f, 2.25f);
        }
        else if (selectedObject == 2)
        {
            GameObject gm = Instantiate(waterBarrelPreview);
            currentObject = gm;
            objectSize = new Vector2(1f, 1f);
        }
        else if (selectedObject == 3)
        {
            GameObject gm = Instantiate(rollingBarrelPreview);
            currentObject = gm;
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
        if(Physics2D.OverlapBox(mousePosition, objectSize, 0f) != null)
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
            switch(selectedObject)
            {
                case 1:
                    SpawnObject(explodingBarrel);
                    break;
                case 2:
                    SpawnObject(waterBarrel);
                    break;
                case 3:
                    SpawnObject(rollingBarrel);
                    break;

            }
        }
    }

    private void SpawnObject(GameObject gm)
    {
        Instantiate(gm, mousePosition, Quaternion.identity);
        selectedObject = 0;
        Destroy(currentObject);
    }
}
