using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
 
    public Rigidbody projectile;            // Used to Create projectile
    public int _ammo;                        // Used to keep track of how much ammo there is
    public Transform projectileSpawnPoint;  // Used to position the bullet once spawned
    public float projectileForce;           // Used to apply force to the bullet being fired

    // Use this for initialization
    void Start () {

        if (ammo <= 0)
        {
            // Set the ammo count to 20
            ammo = 20;
        }

        if (projectileForce <= 0)
        {
            // Set the bullet force
            projectileForce = 3.0f;
        }
	}

    public int Shoot()
    {
        // Check if there is enough ammo
        if (projectile && ammo > 0)
        {
            // Create the bullet if there is enough ammo
            Rigidbody temp = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation) as Rigidbody;

            // Add the force to fire the bullet
            temp.AddForce(transform.forward * projectileForce, ForceMode.Impulse);

            Destroy(temp.gameObject, 2.0f);

            // Remove one ammo count
            ammo--;
        }
        // Do something if there isnt enough ammo
        else
        {
            // Play audio for reload

            // Print message
            Debug.Log("Reload");
        }

        return ammo;
    }
    public int ammo //just creates var ammo with getter and setter 
    {
        get;set;
    }
    // do more than just get and set
    //public int ammo
    //{
    //    get { return _ammo; }
    //    set { _ammo = value; }
    //}
}
