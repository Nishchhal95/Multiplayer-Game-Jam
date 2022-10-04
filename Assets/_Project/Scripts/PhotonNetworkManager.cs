using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonNetworkManager : MonoBehaviourPunCallbacks
{
    // This is Singleton with DontDestroyOnLoad.
    public static PhotonNetworkManager Instance => _instance;
    private static PhotonNetworkManager _instance;

    private const string TEST_ROOM_NAME = "Test_Room";

    public static event Action ConnectedToPhotonServer;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ConnectToPhotonServer();
    }

    private void ConnectToPhotonServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(TEST_ROOM_NAME);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(TEST_ROOM_NAME);
    }

    #region Photon Callbacks

    public override void OnConnectedToMaster()
    {
#if UNITY_EDITOR
        PhotonNetwork.NickName = "Editor";
#else
    PhotonNetwork.NickName = "Build";
#endif
        ConnectedToPhotonServer?.Invoke();
        Debug.Log("Successfully Connected to Photon Server!");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Successfully Created a room");
    }
    
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log($"Failed to Created a room returnCode : {returnCode} and Message : {message}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Successfully Joined a room");
    }
    
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"Failed to Join a room returnCode : {returnCode} and Message : {message}");
    }

    // Executes on Master Client or Server
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Player {newPlayer.NickName} entered the Room");
    }

    // Executes on Master Client or Server
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"Player {otherPlayer.NickName} left the Room");
    }

    // Executes Locally
    public override void OnLeftRoom()
    {
        Debug.Log($"Player left Room");
    }

    #endregion
}
