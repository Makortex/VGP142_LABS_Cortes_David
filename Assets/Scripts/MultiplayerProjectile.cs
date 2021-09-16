using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class MultiplayerProjectile : MonoBehaviourPun
{
    [SerializeField] float projectileSpeed;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawnPoint;

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            TakeInput();
        }
    }
    void TakeInput()
    {
        if (!Input.GetButtonDown("Fire1"))
        {
            return;
        }
        //fire();

        //RPC Call: let all clients know when proj. raycast is fired
        //RpcTarget.AllBuffered for players allways
        //RpcTarget.All for players who were from before

        photonView.RPC("fire", RpcTarget.All);
    }
    [PunRPC]
    void fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(projectileSpawnPoint.position, projectileSpawnPoint.forward, out hit, 10))
        {
            Debug.Log("Hit: " + hit.collider.name);

            if (hit.collider.CompareTag("Enemy"))
            {
                Health h = hit.collider.GetComponent<Health>();
                if (h)
                {
                    h.changeHealth(-10);
                }
            }

        }


        //GameObject projectile = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", projectilePrefab.name),
        //    projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        //projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectileSpeed;
    }
}
