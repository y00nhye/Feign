using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] PlayerPrebs;

    private void Start()
    {
        int num = Random.Range(0, PlayerPrebs.Length);
        PhotonNetwork.Instantiate(PlayerPrebs[num].name, new Vector3(40, 44.5f, 0), Quaternion.Euler(0, 90, 0));
    }
}
