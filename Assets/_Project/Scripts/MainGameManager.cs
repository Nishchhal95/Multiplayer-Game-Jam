using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    private void Start()
    {
        //Spawn Player
        PhotonNetwork.Instantiate("Player", transform.position, Quaternion.identity);
    }
}
