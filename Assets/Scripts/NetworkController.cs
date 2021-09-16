using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkController : MonoBehaviourPunCallbacks
{
    [SerializeField] int gameVersion;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = gameVersion.ToString();

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to region: " + PhotonNetwork.CloudRegion);
    }
}
