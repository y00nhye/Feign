using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceCamController : MonoBehaviour
{
    [SerializeField] Transform player;

    private void Start()
    {
        transform.position = new Vector3(45, 44, player.position.z);
    }

}
