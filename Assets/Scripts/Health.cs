using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public int health;
    [SerializeField] TMP_Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        health = 20;
        healthText.text = health.ToString();
    }
    public void changeHealth(int health)
    {
        this.health += health;
        healthText.text = this.health.ToString();
    }
    public int getHealth()
    {
        return health;
    }
    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
