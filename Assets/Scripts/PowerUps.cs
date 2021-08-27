using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public enum CollectibleType
    {
        REDPILL,
        COIN
    }
    public CollectibleType currentCollectible;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            Character curMovementScript = other.GetComponent<Character>();
            switch (currentCollectible)
            {
                case CollectibleType.REDPILL:
                    other.gameObject.GetComponent<Character>().speedUp();
                    break;
                case CollectibleType.COIN:
                    GameManager.instance.score++;
                    break;
            }
            Destroy(gameObject);

        }

    }
}
