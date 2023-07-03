using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerTransform : MonoBehaviourPun
{
    private void Awake()
    {
        if (!photonView.IsMine) return;

        FindObjectOfType<EntranceCamController>().FollowPlayer();
    }
}
