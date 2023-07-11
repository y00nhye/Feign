using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class EntranceCamController : MonoBehaviourPun
{
    [Header("[Vote Cam]")]
    [SerializeField] CinemachineVirtualCamera voteCam;

    private TimeManager timeManager;

    private void Awake()
    {
        timeManager = FindObjectOfType<TimeManager>();
    }
    
    public void FollowPlayer()
    {
        Invoke("MovetoVoteCam", 10);
    }

    private void MovetoVoteCam()
    {
        GetComponent<CinemachineVirtualCamera>().enabled = false;

        voteCam.enabled = true;

        timeManager.isTimeChange = true;
        timeManager.isVote = true;
    }
}
