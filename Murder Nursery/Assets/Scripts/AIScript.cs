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
    GameObject graceDUPos;

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
            graceDUPos.SetActive(true); 
            gameObject.SetActive(false);
        }


        animator.SetFloat("Velocity", velocity);
    }

    public void SetDestination(Vector3 target)
    {
        agent.SetDestination(target);
        currentDestination = target;
        destinationSet = true;
    }
}
