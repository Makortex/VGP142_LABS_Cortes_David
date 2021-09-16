using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class StartLobbyController : MonoBehaviourPunCallbacks
{
    //Informational UI
    [SerializeField] GameObject statusLabel;
    [SerializeField] GameObject btnStart;
    [SerializeField] GameObject btnCancel;

    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] int roomSize;


    // Start is called before the first frame update
    void Start()
    {
        if (btnStart && btnCancel)
        {
            btnStart.SetActive(false);
            btnStart.GetComponent<Button>().interactable = false;
            btnCancel.SetActive(false);
        }

    }

    public override void OnConnectedToMaster()
    {
        statusLabel.GetComponent<TMP_Text>().text = "Connected to " + PhotonNetwork.CloudRegion;

        btnStart.SetActive(true);

        PhotonNetwork.AutomaticallySyncScene = true;
    }
    //link to 
    public void SetPlayerName(TMP_InputField name)
    {
        Debug.Log("InputField: " + name.text);

        btnStart.GetComponent<Button>().interactable = !string.IsNullOrEmpty(name.text);

        PhotonNetwork.NickName = name.text;
    }
    public void StartGame()
    {
        btnStart.SetActive(false);
        btnCancel.SetActive(true);

        //don't have both
        //PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.JoinRoom("SusRoom");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room join failled: " + message);
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Creating sus Room.");
        //initializer list
        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = (byte)roomSize
        };
        PhotonNetwork.CreateRoom("SusRoom", roomOptions);

        //Random Room Creation
        //int randomRoomNumber = Random.Range(0, 1000);
        //PhotonNetwork.CreateRoom("Room " + randomRoomNumber, roomOptions);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed: " + message);

        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = (byte)roomSize
        };
    }
    public void CancelRoomJoin()
    {
        btnStart.SetActive(true);
        btnCancel.SetActive(false);

        PhotonNetwork.LeaveRoom();
    }
}
