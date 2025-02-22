using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    private int currentPoint;

    void Start()
    {
        transform.position = patrolPoints[0].position;
        currentPoint = 0;
    }

    
    void Update()
    {
        if(transform.position == patrolPoints[currentPoint].position){
            currentPoint++;
        }

        if(currentPoint>=patrolPoints.Length){
            currentPoint = 0;
        }

        transform.position = Vector3.MoveTowards(transform.position,patrolPoints[currentPoint].position,moveSpeed*Time.deltaTime);
    }
}
