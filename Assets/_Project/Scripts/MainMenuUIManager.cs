using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private Image photonConnectionStatusImage;
    [SerializeField] private Color notConnectedColor;
    [SerializeField] private Color connectedColor;

    [SerializeField] private TextMeshProUGUI photonConnectionStatusText;

    [SerializeField] private Button createRoomButton, joinRoomButton;

    private void Start()
    {
        photonConnectionStatusImage.color = notConnectedColor;
        photonConnectionStatusText.SetText("Not Connected");
        createRoomButton.interactable = false;
        joinRoomButton.interactable = false;
    }

    private void OnEnable()
    {
        PhotonNetworkManager.ConnectedToPhotonServer += OnConnectedToPhotonServer;
    }

    private void OnConnectedToPhotonServer()
    {
        photonConnectionStatusImage.color = connectedColor;
        photonConnectionStatusText.SetText("Connected");
        createRoomButton.interactable = true;
        joinRoomButton.interactable = true;
    }

    public void CreateRoomClick()
    {
        PhotonNetworkManager.Instance.CreateRoom();
    }
    
    public void JoinRoomClick()
    {
        PhotonNetworkManager.Instance.JoinRoom();
    }

    private void OnDisable()
    {
        PhotonNetworkManager.ConnectedToPhotonServer -= OnConnectedToPhotonServer;
    }
}
