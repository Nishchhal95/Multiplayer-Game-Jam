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

    [SerializeField] private GameObject userInfoPanel;
    [SerializeField] private GameObject connectionInfoPanel;
    [SerializeField] private TMP_InputField usernameInputField;

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

    public void UserNameSaveClick()
    {
        string localPlayerUserName = usernameInputField.text;
        localPlayerUserName = localPlayerUserName.Trim();
        if (string.IsNullOrEmpty(localPlayerUserName))
        {
            Debug.Log("Username cannot be empty!");
            return;
        }
        PhotonNetworkManager.Instance.SetLocalUserName(localPlayerUserName);
        userInfoPanel.SetActive(false);
        connectionInfoPanel.SetActive(true);
    }

    private void OnDisable()
    {
        PhotonNetworkManager.ConnectedToPhotonServer -= OnConnectedToPhotonServer;
    }
}
