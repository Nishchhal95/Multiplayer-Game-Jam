using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] private Transform[] playerSpawnPoints;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] [CanBeNull] private CinemachineFreeLook cm_FreeLook;
    
    [SerializeField] private GameObject[] bottleGameObjects;
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject portalVFXGO;
    [SerializeField] private GameObject level;

    [SerializeField] private GameObject myGameObject;

    private void Start()
    {
        //Spawn Player
        myGameObject = PhotonNetwork.Instantiate(
            "Player", 
            playerSpawnPoints[PhotonNetworkManager.Instance.playerIndex].position, 
            Quaternion.identity);
        myGameObject.GetComponent<PlayerController>().SetCamera(cameraTransform);
        cm_FreeLook.Follow = myGameObject.transform.GetChild(0);
        cm_FreeLook.LookAt = myGameObject.transform.GetChild(0);
    }

    public void DisableBottle(string bottleName)
    {
        for (int i = 0; i < bottleGameObjects.Length; i++)
        {
            if (bottleGameObjects[i].name.Equals(bottleName))
            {
                bottleGameObjects[i].SetActive(false);
                IncrementScore();
                return;
            }
        }
    }

    private void IncrementScore()
    {
        score++;
        scoreText.SetText($"Potions Collected :{score}");
    }

    public void ShowPortal()
    {
        portalVFXGO.SetActive(true);
    }

    public void LoadFinalScene()
    {
        level.SetActive(false);
        SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
        myGameObject.transform.position = playerSpawnPoints[PhotonNetworkManager.Instance.playerIndex].position;
    }
}
