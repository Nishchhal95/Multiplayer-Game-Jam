using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] private Transform[] playerSpawnPoints;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] [CanBeNull] private CinemachineFreeLook cm_FreeLook;

    private void Awake()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    private void Start()
    {
        //Spawn Player
        GameObject myGameObject = PhotonNetwork.Instantiate(
            "Player", 
            playerSpawnPoints[PhotonNetworkManager.Instance.playerIndex].position, 
            Quaternion.identity);
        myGameObject.GetComponent<PlayerController>().SetCamera(cameraTransform);
        cm_FreeLook.Follow = myGameObject.transform.GetChild(1);
        cm_FreeLook.LookAt = myGameObject.transform.GetChild(1);
    }
}
