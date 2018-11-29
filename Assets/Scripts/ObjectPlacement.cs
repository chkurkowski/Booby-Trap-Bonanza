﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPlacement : MonoBehaviour {

    public GameObject explodingBarrelPreview;
    public GameObject waterBarrelPreview;
    public GameObject rollingBarrelPreview;
    public GameObject chairPreview;
    public GameObject explodingBarrel;
    public GameObject waterBarrel;
    public GameObject rollingBarrel;
    public GameObject chair;
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


    //limited resources stuff
    public Text exBarrelRemaining;     public Text waterBarrelRemaining;     public Text rollingBarrelRemaining;
    public Text tableRemaining;     public int exBarrelNumber = 5;     public int waterBarrelNumber = 5;     public int rollingBarrelNumber = 5;
    public int tableNumber = 5;     private bool canPlaceEX = true;     private bool canPlaceWater = true;     private bool canPlaceRoll = true;
    private bool canPlaceTable = true;


    private bool flipped = true;
    //text popup stuff
    public GameObject exBarrelText;
    public GameObject waterBarrelText;
    public GameObject rollingBarrelText;
    public GameObject tableText;



    // Use this for initialization
    void Start () 
    {
        exBarrelText.SetActive(false);
        waterBarrelText.SetActive(false);
        rollingBarrelText.SetActive(false);
        tableText.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
        GetMousePosition();
        FollowMouse();
        ClickToPlace();
        FlipItem();

        SelectItem();

        if (waterBarrelNumber <= 0)         {             canPlaceWater = false;         }         if (exBarrelNumber <= 0)         {             canPlaceEX = false;         }         if (rollingBarrelNumber <= 0)         {             canPlaceRoll = false;         }
        if(tableNumber <= 0)
        {
            canPlaceTable = false;
        }

    }

    private void SelectItem()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            SelectItem(1);
            exBarrelText.SetActive(true);
            waterBarrelText.SetActive(false);
            rollingBarrelText.SetActive(false);
            tableText.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            SelectItem(2);
            waterBarrelText.SetActive(true);
            exBarrelText.SetActive(false);
            rollingBarrelText.SetActive(false);
            tableText.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            SelectItem(3);
            rollingBarrelText.SetActive(true);
            exBarrelText.SetActive(false);
            waterBarrelText.SetActive(false);
            tableText.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            SelectItem(4);
            tableText.SetActive(true);
            exBarrelText.SetActive(false);
            waterBarrelText.SetActive(false);
            rollingBarrelText.SetActive(false);
        }
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
            objectSize = new Vector2(1f, 2.4f);
        }
        else if (selectedObject == 3)
        {
            GameObject gm = Instantiate(rollingBarrelPreview);
            currentObject = gm;
            objectSize = new Vector2(1.5f, 1.5f);
        }
        else if (selectedObject == 4)
        {
            GameObject gm = Instantiate(chairPreview);
            currentObject = gm;
            objectSize = new Vector2(1.45f, 1.5f);
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
            if (flipped)
            {
                Vector3 scale = currentObject.transform.localScale;
                if(currentObject.transform.localScale.x < 0)
                    currentObject.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            }
            else
            {
                Vector3 scale = currentObject.transform.localScale;
                if (currentObject.transform.localScale.x > 0)
                    currentObject.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            }
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
                case 1:                     if (canPlaceEX == true)                     {                         SpawnObject(explodingBarrel);                         exBarrelNumber--;                         exBarrelRemaining.text = exBarrelNumber.ToString();                     }                     break;                 case 2:                     if (canPlaceWater == true)                     {                         SpawnObject(waterBarrel);                         waterBarrelNumber--;                         waterBarrelRemaining.text = waterBarrelNumber.ToString();                     }                     break;                 case 3:                     if (canPlaceRoll == true)                     {
                        int temp = 1;
                        if (flipped)
                            temp = -1;                         SpawnObject(rollingBarrel, temp);
                        rollingBarrelNumber--;
                        rollingBarrelRemaining.text = rollingBarrelNumber.ToString();                     }                     break;
                case 4:
                    if(canPlaceTable == true)
                    {
                        SpawnObject(chair, flipped);
                        tableNumber--;
                        tableRemaining.text = tableNumber.ToString();
                    }

                    break;
            }
        }

        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Collider2D[] activatables = Physics2D.OverlapCircleAll(mousePosition, .05f);

            foreach(Collider2D col in activatables)
            {
                if(col.gameObject.layer == 8)
                {
                    print("Activated the " + col.name);
                    col.GetComponent<ObjectsScript>().isActive = true;
                }
            }
        }
    }

    private void FlipItem()
    {
        if(Input.GetKeyDown(KeyCode.Space) && selectedObject != 0)
        {
            flipped = !flipped;
        }
    }

    private void SpawnObject(GameObject gm)
    {
        Instantiate(gm, mousePosition, Quaternion.identity);
        selectedObject = 0;
        Destroy(currentObject);
    }

    //Spawns object and passes a direction for the rolling barrel
    private void SpawnObject(GameObject gm, int rolldirection)
    {
        GameObject barrel = Instantiate(gm, mousePosition, Quaternion.identity);
        barrel.GetComponent<RollingBarrel>().rollDirection = rolldirection;
        selectedObject = 0;
        Destroy(currentObject);
    }

    private void SpawnObject(GameObject gm, bool isFlipped)
    {
        GameObject chairGM = Instantiate(gm, mousePosition, Quaternion.identity);
        if(isFlipped)
        {
            chairGM.transform.localScale = new Vector3(-2.25f, 2.25f, 1);
        }
        else
            chairGM.transform.localScale = new Vector3(2.25f, 2.25f, 1);
        selectedObject = 0;
        Destroy(currentObject);
    }
}
