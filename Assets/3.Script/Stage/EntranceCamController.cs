using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EntranceCamController : MonoBehaviour
{
    [Header("[My Player transform]")]
    [SerializeField] Transform player;

    [Header("[Vote Cam]")]
    [SerializeField] CinemachineVirtualCamera voteCam;

    private TimeManager timeManager;

    private void Awake()
    {
        timeManager = FindObjectOfType<TimeManager>();
    }
    private void Start()
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
