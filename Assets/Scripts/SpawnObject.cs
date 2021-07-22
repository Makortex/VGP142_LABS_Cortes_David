using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnObject : MonoBehaviour
{
    public GameObject[] objects;
    public Transform ObjectSpawn;

    // Start is called before the first frame update
    void Start()
    {
        { try
            {
                int RNG = UnityEngine.Random.Range(0, 4);
                Debug.Log("RNG = " + RNG);
                if (RNG != 0)
                {
                    Instantiate(objects[UnityEngine.Random.Range(0, 3)], ObjectSpawn.position, ObjectSpawn.rotation);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }
            finally
            {
                Debug.LogWarning("Always get called");
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
