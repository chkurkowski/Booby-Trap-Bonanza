using System.Collections;
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
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            SelectItem(2);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            SelectItem(3);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            SelectItem(4);
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
                case 1:                     if (canPlaceEX == true)                     {                         SpawnObject(explodingBarrel);                         exBarrelNumber--;                         exBarrelRemaining.text = exBarrelNumber.ToString();                     }                     break;                 case 2:                     if (canPlaceWater == true)                     {                         SpawnObject(waterBarrel);                         waterBarrelNumber--;                         waterBarrelRemaining.text = waterBarrelNumber.ToString();                     }                     break;                 case 3:                     if (canPlaceRoll == true)                     {                         SpawnObject(rollingBarrel, -1);                         rollingBarrelNumber--;                         rollingBarrelRemaining.text = rollingBarrelNumber.ToString();                     }                     break;
                case 4:
                    if(canPlaceTable == true)
                    {
                        SpawnObject(chair, true);
                        tableNumber--;
                        tableRemaining.text = tableNumber.ToString();
                    }

                    break;

            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && currentObject != null && validPos)
        {
            if(selectedObject == 3)
            {
                if(canPlaceRoll == true)
                {
                    SpawnObject(rollingBarrel, 1);
                    rollingBarrelNumber--;
                    rollingBarrelRemaining.text = rollingBarrelNumber.ToString(); 
                }
            }
            else if(selectedObject == 4)
            {
                if(canPlaceTable)
                {
                    SpawnObject(chair, false);
                    tableNumber--;
                    tableRemaining.text = tableNumber.ToString();
                }
            }
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

    private void SpawnObject(GameObject gm, bool flipped)
    {
        GameObject chairGM = Instantiate(gm, mousePosition, Quaternion.identity);
        if(flipped)
        {
            chairGM.transform.localScale = new Vector3(-2.25f, 2.25f, 1);
        }
        else
            chairGM.transform.localScale = new Vector3(2.25f, 2.25f, 1);
        selectedObject = 0;
        Destroy(currentObject);
    }
}
