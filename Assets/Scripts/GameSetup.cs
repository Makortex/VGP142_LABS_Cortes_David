using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class GameSetup : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyPrefab;


    GameObject[] enemySpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");

        CreatePlayer();
        //CreateEnemies();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            CreateEnemies();
    }
    void CreatePlayer()
    {
        //player prefab must be in folder called Resources
        //GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);

        GameObject player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", playerPrefab.name), Vector3.zero, Quaternion.identity);

        player.name = PhotonNetwork.NickName;
    }
    void CreateEnemies()
    {
        Debug.Log("Creating " + enemySpawnPoints.Length + "enemies");
        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            GameObject enemy = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", enemyPrefab.name),
            enemySpawnPoints[i].transform.position, Quaternion.identity);
        }
            

    }
}
