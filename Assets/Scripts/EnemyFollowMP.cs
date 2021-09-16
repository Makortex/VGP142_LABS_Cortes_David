using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class EnemyFollowMP : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;
    //Animator animator;
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        //animator.applyRootMotion = false;

        rigidbody.isKinematic = true;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        if (!target)
        {
            target = GameObject.FindWithTag("Player");
        }
        agent.SetDestination(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.transform.position);
        //is allways moving
        //animator.SetFloat("Speed", agent.speed);

        //not allways
        //animator.SetFloat("Speed", transform.InverseTransformDirection(agent.velocity).z);

        //checks if agent is on blackline
        //animator.SetBool("IsGrounded", !agent.isOnOffMeshLink);
    }
}
