using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    [Header("[PlayerPrebs]")]
    public GameObject[] PlayerPrebs;

    [Header("[player Vote Pos]")]
    [SerializeField] Transform[] playerPos;

    [Header("[Cinemachine Camera]")]
    [SerializeField] CinemachineVirtualCamera nightCam;

    [Header("[Camera Position]")]
    [SerializeField] Transform[] nightCamPos;

    private RoleConverter roleConverter;

    private bool isSpawn = false;

    private void Awake()
    {
        roleConverter = FindObjectOfType<RoleConverter>();
    }
    private void Update()
    {
        if (roleConverter.isFinish && !isSpawn)
        {
            isSpawn = true;
            PlayerSpawn();
        }
    }
    public void PlayerSpawn()
    {
        int playerNum = (int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"];

        int num = Random.Range(0, PlayerPrebs.Length);
        GameObject player = PhotonNetwork.Instantiate(PlayerPrebs[num].name, playerPos[playerNum].position, playerPos[(int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]].rotation);

        nightCam.Follow = nightCamPos[playerNum];
        nightCam.LookAt = nightCamPos[playerNum];
    }
}
