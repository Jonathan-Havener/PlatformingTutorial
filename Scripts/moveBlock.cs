using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBlock : MonoBehaviour
{

    public int speed;
    float distanceThreshold = (float)0.5;

    public List<GameObject> wayPoints = new List<GameObject>();
    Queue<GameObject> navigationPoints = new Queue<GameObject>();

    GameObject nextWayPoint;
    public bool loop;

    void Start()
    {
        foreach(GameObject point in wayPoints)
        {
            navigationPoints.Enqueue(point);
        }

        //set first waypoint
        if(navigationPoints.Count>0)
        {
            nextWayPoint = navigationPoints.Dequeue();
        }
    }

    void Update()
    {
        //find the direction towards the next waypoint
        Vector3 direction = nextWayPoint.transform.position - gameObject.transform.position;

        // if we've reached the most recent waypoint
        if (direction.magnitude < distanceThreshold)
        {
            if (loop)
            {
                navigationPoints.Enqueue(nextWayPoint);
            }
            if (navigationPoints.Count > 0)
            {
                //enqueue the next point
                nextWayPoint = navigationPoints.Dequeue();
            }
        }
        else
        {
            //scale the vector down to a standard size (normalize the vector)
            direction = direction / direction.magnitude;

            //move towards next point at speed
            gameObject.transform.Translate(direction * speed * Time.deltaTime);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent = null;
        }
    }


}
