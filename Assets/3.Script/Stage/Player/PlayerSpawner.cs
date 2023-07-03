using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    [Header("[PlayerPrebs]")]
    public GameObject[] PlayerPrebs;

    [Header("[player Vote Pos]")]
    [SerializeField] Transform[] playerPos;

    private void Start()
    {
        int num = Random.Range(0, PlayerPrebs.Length);
        PhotonNetwork.Instantiate(PlayerPrebs[num].name, playerPos[(int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]].position, playerPos[(int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]].rotation);
    }
}
