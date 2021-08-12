using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            //Destroy(gameObject);
            animator.SetBool("WasShot", true);

        }
        if (collision.gameObject.tag == "PlayerPunch")
        {
            //Destroy(gameObject);
            animator.SetBool("WasPunched", true);
            Debug.Log("punch");

        }
        if (collision.gameObject.tag == "PlayerKick")
        {
            //Destroy(gameObject);
            animator.SetBool("WasKicked", true);
            Debug.Log("kick");
        }

    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (gameObject.tag == "PlayerPunch")
    //    {
    //        //Destroy(gameObject);
    //        animator.SetBool("WasPunched", true);
    //        Debug.Log("punch");

    //    }
    //}
}
