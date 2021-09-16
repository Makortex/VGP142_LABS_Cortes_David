using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class StartRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField] string multiplayerSceneName;

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined: " + PhotonNetwork.CurrentRoom.Name);

        StartGame();

    }
    void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(multiplayerSceneName);
        }
    }
}
