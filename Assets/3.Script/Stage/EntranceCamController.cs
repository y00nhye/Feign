using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class EntranceCamController : MonoBehaviourPun
{
    [Header("[My Player transform (set)]")]
    public Transform player;

    [Header("[Vote Cam]")]
    [SerializeField] CinemachineVirtualCamera voteCam;

    private TimeManager timeManager;

    private void Awake()
    {
        timeManager = FindObjectOfType<TimeManager>();
    }
    
    public void FollowPlayer()
    {
        transform.position = new Vector3(45, 44, player.position.z);

        Invoke("MovetoVoteCam", 9);
    }

    private void MovetoVoteCam()
    {
        GetComponent<CinemachineVirtualCamera>().enabled = false;

        voteCam.enabled = true;

        timeManager.isTimeChange = true;
        timeManager.isVote = true;
    }
}
