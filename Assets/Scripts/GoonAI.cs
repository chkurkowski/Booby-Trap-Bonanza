using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoonAI : MonoBehaviour {

    public enum State{
        IDLE,
        PATROL,
        ALERT,
        BURNING,
        SLIDING,
        DYING
    }
    public State state;

    public bool move = true;

    public bool alive;

    private float idleTimer = 0f;
    [SerializeField]
    private float IDLETIME = 2f;

    [SerializeField]
    private int patrolDirection = -1;
    public  Vector2[] waypoints;
    private Vector2 target;
    private float speed = 2;
    private int currentWaypoint = 0;
    private int currentDirection;

    private bool moving = false;
    private bool burning = false;
    private bool sliding = false;

    private Animator animatorInfo;

	// Use this for initialization
	void Start () {
        alive = true;
        state = State.IDLE;
        if (move)
            RandomizePosition();
        animatorInfo = gameObject.GetComponent<Animator>();

        StartCoroutine("FSM");
	}
	
    /* Finite State Machine and State Functions */

    IEnumerator FSM()
    {
        while(alive)
        {
            print(state);

            switch (state)
            {
                case State.IDLE:
                    Idle();
                    break;
                case State.PATROL:
                    Patrol();
                    break;
                case State.ALERT:
                    Alert();
                    break;
                case State.BURNING:
                    Burning();
                    break;
                case State.SLIDING:
                    Sliding();
                    break;
                case State.DYING:
                    Dying();
                    break;
            }
            yield return null;
        }
    }

    private void Idle()
    {
        animatorInfo.SetBool("isIdle", true);
        animatorInfo.SetBool("moveRight", false);
        animatorInfo.SetBool("moveLeft", false);
        moving = false;
        idleTimer += Time.deltaTime;

        if(idleTimer >= IDLETIME)
        {
            state = State.PATROL;
            idleTimer = 0;
        }
    }

    private void Patrol()
    {
        if (move)
        {
            float distance = Vector2.Distance(transform.position, target);
            float dirDist = transform.position.x - target.x;
            float step = speed * Time.deltaTime;
            moving = true;

            //TODO Set animation here.
            transform.position = Vector2.MoveTowards(transform.position, target, step);


            if (distance < .001f)
            {
                if (patrolDirection < 0)
                {
                    DecrementTarget();
                }
                else
                {
                    IncrementTarget();
                }
                state = State.IDLE;

            }

            if (dirDist < 0)
            {
                currentDirection = 1;
                //animatorInfo.SetBool("moveLeft", false);
                animatorInfo.SetBool("isIdle", false);
                animatorInfo.SetBool("moveRight", true);
            }
            else
            {
                currentDirection = -1;
                //  animatorInfo.SetBool("moveRight", false);
                animatorInfo.SetBool("isIdle", false);
                animatorInfo.SetBool("moveLeft", true);

            }

        }

    }

    private void Alert()
    {

    }

    private void Burning()
    {
       // animatorInfo.SetBool("isBurning", true);
        speed = .1f;
        Invoke("BurningDeath", 4);
        burning = true;

        if(currentDirection > 0)
        {
            //animatorInfo.SetFloat("speed", 1);
            animatorInfo.SetBool("moveLeft", false);
            animatorInfo.SetBool("moveRight", true);

            transform.Translate(Vector3.right * speed);
        }
            
        else
        {
            //animatorInfo.SetFloat("speed", -1);
            animatorInfo.SetBool("moveRight", false);
            animatorInfo.SetBool("moveLeft", true);

            transform.Translate(-Vector3.right * speed);
        }
            

        gameObject.layer = 0;
        gameObject.tag = "Interactable";
    }

    private void Sliding()
    {
        speed = .08f;
        sliding = true;

        if (currentDirection > 0)
            transform.Translate(Vector3.right * speed);
        else
            transform.Translate(-Vector3.right * speed);

        gameObject.layer = 0;
        gameObject.tag = "Interactable";
    }

    private void Dying()
    {
        alive = false;
        print(alive);
    }

    /* Other Member Functions to be used */

    private void RandomizePosition()
    {
        currentWaypoint = Random.Range(0, waypoints.Length - 1);
        target = waypoints[currentWaypoint];
        int temp = currentWaypoint + 1;
        transform.position = waypoints[temp];
    }

    private void IncrementTarget()
    {
        print("Current Waypoint Before: " + currentWaypoint);
        if (target == waypoints[waypoints.Length - 1])
        {
            target = waypoints[0];
            currentWaypoint = 0;
        }
        else
        {
            target = waypoints[currentWaypoint++];
           
        }
        print("Current Waypoint After: " + currentWaypoint);
    }

    private void DecrementTarget()
    {
        if (target == waypoints[0])
        {
            target = waypoints[waypoints.Length - 1];
            currentWaypoint = waypoints.Length - 1;
        }
        else
        {
            target = waypoints[currentWaypoint--];
        }
    }

    private void BurningDeath()
    {
        animatorInfo.SetBool("isAsh", true);
        state = State.DYING;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((sliding && col.gameObject.layer == 8) || (sliding && col.gameObject.tag == "Wall"))
        {
            animatorInfo.SetBool("isKill", true);
            state = State.DYING;
        }

        if (col.gameObject.name == "WaterPuddle(Clone)")
            {
                animatorInfo.SetBool("isSlip", true);
                state = State.SLIDING;
               
            }
            else if (col.gameObject.name == "FlameWoosh(Clone)")
            {
                animatorInfo.SetBool("isBurning", true);
                state = State.BURNING;
            }
            else if (col.gameObject.name == "BarrelExplosion(Clone)")
            {
                animatorInfo.SetBool("isKill", true);
                state = State.DYING;
            }
            else if (col.gameObject.name == "ChairLeg(Clone)")
            {
                animatorInfo.SetBool("isImpale", true);
                state = State.DYING;
            }
            else if (col.gameObject.name == "Chandelier")
            {
                animatorInfo.SetBool("isCrush", true);
                state = State.DYING;
            }
            else if (col.gameObject.name == "RollingBarrel(Clone)") 
            {
                animatorInfo.SetBool("isKill", true);
                state = State.DYING;
            }
        }


    private void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("This collision happened");
        
            if (burning)
            {
                if (col.gameObject.tag == "Wall")
                {
                    print("Hit Wall");
                    if (currentDirection < 0)
                        currentDirection = 1;
                    else
                        currentDirection = -1;
                }
            }

            if ((sliding && col.gameObject.layer == 8) || (sliding && col.gameObject.tag == "Wall"))
            {
            Debug.Log("Ummm");
                Destroy(gameObject);
            }
      
            
            if (col.gameObject.name == "WaterPuddle(Clone)")
            {
                animatorInfo.SetBool("isSlip", true);
                state = State.SLIDING;
            }
            else if (col.gameObject.name == "FlameWoosh(Clone)")
            {
                animatorInfo.SetBool("isBurning", true);
                state = State.BURNING;
            }
            else if (col.gameObject.name == "BarrelExplosion(Clone)")
            {
                animatorInfo.SetBool("isKill", true);
                state = State.DYING;
            }
            else if (col.gameObject.name == "ChairLeg(Clone)")
            {
                animatorInfo.SetBool("isImpale", true);
                state = State.DYING;
            }
            else if (col.gameObject.name == "Chandelier")
            {
                animatorInfo.SetBool("isCrush", true);
                state = State.DYING;
            }
            else if (col.gameObject.name == "RollingBarrel(Clone)")
            {
                animatorInfo.SetBool("isKill", true);
                state = State.DYING;
            }
    }


}
