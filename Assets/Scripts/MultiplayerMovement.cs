using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class MultiplayerMovement : MonoBehaviourPun
{
    CharacterController controller;
    [SerializeField] float speed = 5.0f;
    [SerializeField] float jumpSpeed = 2.0f;
    [SerializeField] float rotationSpeed = 10.0f;
    [SerializeField] float gravity = 20.0f;

    Vector3 movement = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
            TakeInput();
    }
    void TakeInput()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0);
        if (controller.isGrounded)
        {
            movement = new Vector3(0, 0, Input.GetAxis("Vertical"));

            movement = transform.TransformDirection(movement);
            movement *= jumpSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                movement.y = jumpSpeed;
            }
        }
        movement.y -= gravity * Time.deltaTime;
        controller.Move(movement * Time.deltaTime);
    }
}
