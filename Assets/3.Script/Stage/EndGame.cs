using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EndGame : MonoBehaviourPunCallbacks
{
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Lobby");
    }
}
