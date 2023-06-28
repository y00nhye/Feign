using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerNameUI : MonoBehaviourPunCallbacks
{
    [Header("[PhotonView]")]
    public PhotonView PV;

    private LobbyBtnController lobbyBtnController;
    private ColorController colorController;

    private void Awake()
    {
        lobbyBtnController = FindObjectOfType<LobbyBtnController>();
        colorController = FindObjectOfType<ColorController>();
    }
    private void Start()
    {
        if (PV.IsMine)
        {
            PV.RPC("Set", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void Set()
    {
        transform.SetParent(GameObject.Find("Sort").transform);
        GetComponentInChildren<Text>().text = PV.Controller.NickName;

        lobbyBtnController.playerColor = GetComponent<Image>();
        colorController.playerColor = GetComponent<Image>();

        colorController.DefaultColor();
    }
}
