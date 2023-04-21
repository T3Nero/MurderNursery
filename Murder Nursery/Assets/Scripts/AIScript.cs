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

    public GameObject grace;

    public GameObject graceDU;

    public GameObject graceLD;

    public GameObject graceNotebook;

    public GameObject[] patrolPoints;

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
        if(destinationSet)
        {
            velocity += Time.deltaTime * agent.acceleration;
            velocity = Mathf.Clamp(velocity, 0, 1);
            
        }

        if ((transform.position - currentDestination).magnitude < agent.stoppingDistance)
        {
            velocity -= Time.deltaTime * agent.acceleration;
            velocity = Mathf.Clamp(velocity, 0, 1);
            destinationSet = false;
        }

        if(destinationSet == false && currentDestination == patrolPoints[0].transform.position)
        {
            graceDU.SetActive(true); 
            grace.SetActive(false);
        }

        if(destinationSet == false && currentDestination == patrolPoints[1].transform.position)
        {
            graceLD.SetActive(true); 
            grace.SetActive(false);
        }

        if(destinationSet == false && currentDestination == patrolPoints[2].transform.position)
        {
            graceNotebook.SetActive(true);
            grace.SetActive(false);
        }


        animator.SetFloat("Velocity", velocity);
    }

    public void SetDestination(Vector3 target)
    {
        grace.SetActive(true);
        agent.SetDestination(target);
        currentDestination = target;
        destinationSet = true;
    }
}
