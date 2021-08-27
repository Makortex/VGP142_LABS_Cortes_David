using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyDrops : MonoBehaviour
{
    public GameObject [] enemyDrop;
    public Transform PUpSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DropPUp()
    {
        int RNG = UnityEngine.Random.Range(0, enemyDrop.Length+1);
        Debug.Log("RNG = " + RNG);
        if (RNG != 0)
        {
            Instantiate(enemyDrop[UnityEngine.Random.Range(0, enemyDrop.Length)], PUpSpawn.position, PUpSpawn.rotation);
        }
        Destroy(gameObject);
    }
}
