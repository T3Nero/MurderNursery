using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;

    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject[] patrolPoints;

    bool destinationSet;
    float velocity;
    Vector3 currentDestination;

    private void Start()
    {
        //SetDestination(patrolPoints[0].transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetDestination(patrolPoints[0].transform.position);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetDestination(patrolPoints[1].transform.position);
        }
        
        if(destinationSet)
        {
            velocity += Time.deltaTime * agent.acceleration;
            velocity = Mathf.Clamp(velocity, 0, 1);
            
        }

        if((transform.position - currentDestination).magnitude <= agent.stoppingDistance)
        {
            velocity -= Time.deltaTime * agent.acceleration;
            velocity = Mathf.Clamp(velocity, 0, 1);
            destinationSet = false;
        }


        animator.SetFloat("Velocity", velocity);
    }

    void SetDestination(Vector3 target)
    {
        agent.SetDestination(target);
        currentDestination = target;
        destinationSet = true;
    }
}
