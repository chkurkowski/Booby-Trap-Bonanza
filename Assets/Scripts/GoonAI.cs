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
    private Vector2[] waypoints;
    private Vector2 target;
    private float speed = 2;
    private int currentWaypoint = 0;
    private int patrolDirection = -1;

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
        float step = speed * Time.deltaTime;

        //TODO: Instead of this approach, check direction of the next waypoint then have the goon transform.Translate to it.
        //Set animation here.
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
    }

    private void Alert()
    {

    }

    private void Burning()
    {

    }

    private void Sliding()
    {

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
}
