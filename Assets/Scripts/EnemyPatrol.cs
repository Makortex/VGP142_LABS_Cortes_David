using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyPatrol : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;
    Animator animator;
    Rigidbody rigidbody;

    // used to populate path
    //PN = patrol nodes
    //false: PN manually added to array
    //true: PN auto added to array
    public bool autoGenPath;

    //name of tag to look
    public string pathNodeName = "PatrolNode";

    // move to next PN based on distance or trigger
    //false: use trigger based
    //true: use distance based
    public bool distanceBased;

    public float distanceToNextNode = 1.0f;

    //used to hold all PN enemyAI will use
    public GameObject[] patrolPath;

    //keep track of what enemyAI is using in array
    public int patrolPathIndex = 0;

    public float waitTime = 0.0f;
    public bool isWaiting;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        animator.applyRootMotion = false;

        rigidbody.isKinematic = true;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        //check if pathNodeName is set
        if (pathNodeName == string.Empty)
        {
            //set name to default path name
            pathNodeName = "PatrolNode";
        }
        //finds all PN in scene and adds them to array
        //~order is based in the addition order
        //overwrites patrolPath if PN were added manually
        if (autoGenPath)
        {
            patrolPath = GameObject.FindGameObjectsWithTag(pathNodeName);
        }
        //use patrolNode specified by 'patrolPathIndex'
        if (patrolPath.Length > 0)
        {
            target = patrolPath[patrolPathIndex];
        }
        else
        {
            target = transform.gameObject;
        }
        //tell EnemyAI to move towards target
        agent.SetDestination(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //check if is using 
        if (distanceBased)
        {
            //if (Vector3.Distance(target.transform.position, transform.position)<distanceToNextNode)
            //if ((target.transform.position - transform.position).magnitude < distanceToNextNode)
            if (!isWaiting && agent.remainingDistance < distanceToNextNode)
            {
                StartCoroutine("WaitToMove");

            }
        }
        //check if 'target' has been set
        if (target)
        {
            agent.SetDestination(target.transform.position);

        }
        //not allways
        animator.SetFloat("Speed", transform.InverseTransformDirection(agent.velocity).z);

        //checks if agent is on blackline
        animator.SetBool("IsGrounded", !agent.isOnOffMeshLink);
    }
    IEnumerator WaitToMove()
    {
        isWaiting = true;

        yield return new WaitForSeconds(waitTime);

        isWaiting = false;

        GoToNextNode();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!distanceBased && other.gameObject.CompareTag(pathNodeName))
        {
            GoToNextNode();
        }
    }
    private void GoToNextNode()
    {
        //set 'target' to next PatroNode in PatrolPath
        //can be randomized to create random paths
        patrolPathIndex++;

        patrolPathIndex %= patrolPath.Length;

        // same as above (diferent method)
        //if (patrolPathIndex >= patrolPath.Length)
        //{
        //    patrolPathIndex = 0;
        //}

        //set 'target' to PN specified by 'patrolPathIndex'
        target = patrolPath[patrolPathIndex];

    }
}
