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

    private bool alive;

    private float idleTimer = 0f;
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

	// Use this for initialization
	void Start () {
        alive = true;
        state = State.IDLE;
        RandomizePosition();


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
        float distance = Vector2.Distance(transform.position, target);
        float dirDist = transform.position.x - target.x;
        float step = speed * Time.deltaTime;
        moving = true;

        //TODO Set animation here.
        transform.position = Vector2.MoveTowards(transform.position, target, step);


        if(distance < .001f)
        {
            if(patrolDirection < 0)
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
        }
        else
            currentDirection = -1;
    }

    private void Alert()
    {

    }

    private void Burning()
    {
        speed = .1f;
        Invoke("BurningDeathAnim", 4);
        burning = true;

        if(currentDirection > 0)
            transform.Translate(Vector3.right * speed);
        else
            transform.Translate(-Vector3.right * speed);

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

    private void BurningDeathAnim()
    {
        //TODO burning death anim
        Destroy(gameObject);
    }

    //private void StayOnGround()
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, 10);

    //    if(hit.collider.tag == "Ground")
    //    {
    //        float groundDist = hit.distance;
    //        float tempY = hit.distance - transform.GetComponent<Collider2D>().bounds.extents.y;
    //        transform.position = new Vector3(transform.position.x, tempY, transform.position.z);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "WaterPuddle(Clone)")
        {
            //TODO Slide animation
            state = State.SLIDING;
        }
        else if (col.gameObject.name == "FlameWoosh(Clone)")
        {
            //TODO Running on fire animation
            state = State.BURNING;
        }
        else if (col.gameObject.name == "BarrelExplosion(Clone")
        {
            //TODO Explosion death animation
        }
        else if (col.gameObject.name == "ChairLeg(Clone)")
        {
            //TODO Chair leg death here
        }
        else if (col.gameObject.name == "Chandelier(Clone)")
        {
            //TODO Chandelier death here
        }
        else if(col.gameObject.name == "RollingBarrel(Clone)")
        {
            //TODO Rolling barrel death anim
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (burning)
        {
            print("Hit");
            if (col.gameObject.tag == "Wall")
            {
                print("Hit Wall");
                if (currentDirection < 0)
                    currentDirection = 1;
                else
                    currentDirection = -1;
            }
        }

        if(sliding)
        {
            Destroy(gameObject);
        }
    }
}
