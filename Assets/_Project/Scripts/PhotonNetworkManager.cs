using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PhotonNetworkManager : MonoBehaviourPunCallbacks
{
    // This is Singleton with DontDestroyOnLoad.
    public PhotonNetworkManager Instance => _instance;
    private PhotonNetworkManager _instance;
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

    private void CreateRoom()
    {
        
    }

    private void JoinRoom()
    {
        
    }

    #region Photon Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("Successfully Connected to Photon Server!");
    }

    #endregion
}
