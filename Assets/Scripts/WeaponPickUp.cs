using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class WeaponPickUp : MonoBehaviour {

    public Weapon weapon;               // Used to store the Weapon that gets picked up, must have "Weapon.CS" attached
    public Weapon weaponA;
    public Weapon weaponB;
    private Inventory inventory;

    public GameObject weaponAttach;     // Used to place Weapon Object in Player
    public float weaponDropForce;       // Used to add a force to the 'Weapon' when dropped

    public TMP_Text ammoText;           // Used to show ammo left when a 'Weapon' is active

	// Use this for initialization
	void Start () 
    {
        // Weapon should be left empty
        // - Remove this line if the weapon is manually added as a child of "WeaponPlacement"
        weapon = null;
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        // Check if weaponAttach exists and was dragged into variable
        if (!weaponAttach)
        {
            // Find the point to attach the weapon to
            // - WeaponPlacement is an Empty GameObject used to connect weapon to
            weaponAttach = GameObject.Find("WeaponPlacement");
        }

        if (weaponDropForce <= 0)
        {
            weaponDropForce = 10.0f;

            Debug.Log("WeaponDropForce not set on " + name + ". Defaulting to " + weaponDropForce);
        }

        // Do not display ammo because there is no weapon on "Character"
        if(!weapon)
            ammoText.text = string.Empty;
    }

    // Update is called once per frame
    void Update () {
	    
        // Drop weapon when 'T' is pressed
	    if(Input.GetKeyDown(KeyCode.T))
	    {
		    // Is there a weapon to drop
		    if(weapon)
		    {
                // Remove weapon as a Child of Player
                weaponAttach.transform.DetachChildren();

                // Turn collision back on
                StartCoroutine(EnableCollisions(1.0f));

                // Turn Physics back on	
                weapon.GetComponent<Rigidbody>().isKinematic = false;

                // Throw Weapon forward
                weapon.GetComponent<Rigidbody>().AddForce(weaponA.transform.forward * weaponDropForce, ForceMode.Impulse);

                // Do not display ammo because there is no weapon on "Character"
                if (ammoText)
                    ammoText.text = string.Empty;
            }
	    }

	    // Check if the Fire key was pressed
	    if(Input.GetButtonDown("Fire1"))
	    {
		    // Check if there is weapon attached to the Player
		    if(weapon)
            {
                // Print ammo to screen
                ammoText.text = weapon.Shoot().ToString();
		    }
	    }
        if (Input.GetKeyDown(KeyCode.C)) //change weapon
        {
            if (weapon == weaponA)
            {
                weapon = weaponB;
            }
            if (weapon == weaponB)
            {
                weapon = weaponA;
            }
        }
	}

    // Must set Collider to isTrigger to function
    void OnTriggerEnter(Collider other)
    {
        // Did the player collide with a weapon
        if (other.gameObject.CompareTag("Weapon"))
        {
            // Store a copy of Weapon
            

            // Stop applying Physics to Weapon	
            

            // Move Weapon to weaponAttach position
            

            // Make weaponAttach the parent of Weapon
            

            // Rotate it to the parent identity
            

            // Stop Collision between Player and Weapon
            
        }	
    }

    // Does not work with the Character Controller
    // - Need to add a Collider and a Rigidbody to Character
    void OnCollisionEnter(Collision c)
    {
	
	
    }

    // Used when working with a Character Controller to check for collision
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
	    // Did 'Player' collide with a GameObject tagged as "Weapon"
	    if(/*!weapon &&*/ hit.collider.CompareTag("Weapon"))
	    {
            if (!weaponA)
            {
                //weapon = weaponA;
                // Store a copy of Weapon
                weaponA = hit.gameObject.GetComponent<Weapon>();

                // Stop applying Physics to Weapon	
                weaponA.GetComponent<Rigidbody>().isKinematic = true;

                // Move Weapon to weaponAttach position
                weaponA.transform.position = weaponAttach.transform.position;

                // Make weaponAttach the parent of Weapon
                weaponA.transform.SetParent(weaponAttach.transform);

                // Rotate it to the parent identity
                weaponA.transform.localRotation = weaponAttach.transform.localRotation;

                // Stop Collision between Player and Weapon
                Physics.IgnoreCollision(weaponA.transform.GetComponent<Collider>(), transform.GetComponent<Collider>(), true);
            }
            if (weaponA)
            {
                weapon = weaponA;
                // Store a copy of Weapon
                weaponB = hit.gameObject.GetComponent<Weapon>();

                // Stop applying Physics to Weapon	
                weaponB.GetComponent<Rigidbody>().isKinematic = true;

                // Move Weapon to weaponAttach position
                weaponB.transform.position = weaponAttach.transform.position;

                // Make weaponAttach the parent of Weapon
                weaponB.transform.SetParent(weaponAttach.transform);

                // Rotate it to the parent identity
                weaponB.transform.localRotation = weaponAttach.transform.localRotation;

                // Stop Collision between Player and Weapon
                Physics.IgnoreCollision(weaponB.transform.GetComponent<Collider>(), transform.GetComponent<Collider>(), true);
            }
        }

    }

    IEnumerator EnableCollisions(float timeToDisable)
    {
        // Wait for a specified amount of time
        yield return new WaitForSeconds(timeToDisable);
        //return null;

        // Turn collision back on after timeToDisable seconds
        Physics.IgnoreCollision(weapon.transform.GetComponent<Collider>(), transform.GetComponent<Collider>(), false);


        // Reset weapon to null so a new weapon can be collected
        weapon = null;
    }
}
