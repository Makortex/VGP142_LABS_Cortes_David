using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent (typeof(Animator))]
public class Character : MonoBehaviour
{
    CharacterController controller;
    Animator animator;

    [Header("PlayerSettings")]
    public float speed;
    public float jumpSpeed;
    public float rotationSpeed;
    public float gravity;

    Vector3 moveDirection;

    enum ControllerType { SimpleMove, Move };
    [SerializeField] ControllerType type;

    [Header("ProjectileSettings")]
    public float projectileForce;
    public Rigidbody projectilePrefab;
    public Transform projectieSpawnPoint;

    public Transform thingToLookFrom;
    public float lookAtDistance;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();

            controller.minMoveDistance = 0.0f;

            animator.applyRootMotion = false;

            if (speed <= 0)
            {
                speed = 6.0f;
                //Debug.LogWarning(name + ": Speed not set. Defaulting to" + speed);
            }
            if (jumpSpeed <= 0)
            {
                jumpSpeed = 6.0f;
                //Debug.LogWarning(name + ": jumpSpeed not set. Defaulting to" + jumpSpeed);
            }
            if (rotationSpeed <= 0)
            {
                rotationSpeed = 10.0f;
                //Debug.LogWarning(name + ": rotationSpeed not set. Defaulting to" + rotationSpeed);
            }
            if (gravity <= 0)
            {
                gravity = 9.81f;
                //Debug.LogWarning(name + ": gravity not set. Defaulting to" + gravity);
            }
            if (projectileForce <= 0)
            {
                projectileForce = 10.0f;
            }
            if (!projectilePrefab)
            {
                Debug.LogWarning(name + ":Mising projectilePrefab");
            }
            if (lookAtDistance <= 0)
            {
                //Debug.LogWarning(name + ":Mising projectilePrefab");
                lookAtDistance = 10.0f;
            }

            moveDirection = Vector3.zero;
        }
        catch (NullReferenceException e)
        {
            Debug.LogWarning(e.Message);
        }
        catch (UnassignedReferenceException e)
        {
            Debug.LogWarning(e.Message);
        }
        finally
        {
            Debug.LogWarning("Always get called");
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case ControllerType.SimpleMove:

                //transform.Rotate(0, Input.GetAxis("Horizontal")*rotationSpeed, 0);
                controller.SimpleMove(transform.forward * Input.GetAxis("Vertical") * speed);

                break;

            case ControllerType.Move:

                if (controller.isGrounded)
                {
                    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    moveDirection *= speed;
                    moveDirection = transform.TransformDirection(moveDirection);

                    if (Input.GetButtonDown("Jump"))
                    {
                        moveDirection.y = jumpSpeed;
                    }
                }
                moveDirection.y -= gravity * Time.deltaTime;

                controller.Move(moveDirection * Time.deltaTime);

                break;
        }
        //usage raycast
        //~gameobject needs a collider
        RaycastHit hit;

        if (!thingToLookFrom)
        {
            Debug.DrawRay(transform.position, transform.forward * lookAtDistance, Color.red);

            if (Physics.Raycast(transform.position, transform.forward, out hit, lookAtDistance))
            {
                Debug.Log("Raycast hit: " + hit.transform.name);
            }
        }
        else
        {
            Debug.DrawRay(thingToLookFrom.transform.position, thingToLookFrom.transform.forward * lookAtDistance, Color.blue);

            if (Physics.Raycast(thingToLookFrom.transform.position, thingToLookFrom.transform.forward, out hit, lookAtDistance))
            {
                Debug.Log("Raycast hit: " + hit.transform.name);
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            animator.SetTrigger("Fire");
        }
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Punch");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Kick");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("Dead", true);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetBool("Dead", false);
        }
        animator.SetBool("IsGrounded", controller.isGrounded);
        animator.SetFloat("Speed", transform.InverseTransformDirection(controller.velocity).z);
    }
    //OnCollision 
    //~boh need collider
    //~at least one needs rigidbody
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("OnCollisionEnter: " + collision.transform.name);
    }
    void OnCollisionStay(Collision collision) // As long as it is inside
    {
        //Debug.Log("OnCollisionStay: " + collision.transform.name);
    }
    void OnCollisionExit(Collision collision) // when it stops
    {
        //Debug.Log("OnCollisionExit: " + collision.transform.name);
    }
    //OnControllerColliderHit
    //~behaves like stay
    //~one GO needs a collider
    //~needs character controller
    void OnControllerColliderHit(ControllerColliderHit hit)//same as stay
    {
        //Debug.Log("OnControllerColliderHit: " + hit.transform.name);
    }
    //OnTriggerAAAAA
    //~one GO needs a collider
    //~needs isTrigger
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter: " + other.transform.name);
    }
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("OnTriggerStay: " + other.transform.name);
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("OnTriggerExit: " + other.transform.name);
    }

    public void fire()
    {
        Debug.Log("pew pew");

        if (projectieSpawnPoint && projectilePrefab)
        {
            Rigidbody temp = Instantiate(projectilePrefab, projectieSpawnPoint.position, projectieSpawnPoint.rotation);

            temp.AddForce(projectieSpawnPoint.forward * projectileForce, ForceMode.Impulse);

            Destroy(temp.gameObject, 2.0f);
        }
    }
}