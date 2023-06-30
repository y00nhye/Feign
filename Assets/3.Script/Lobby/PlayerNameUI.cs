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

    private Image playerColor;

    private LobbyBtnController lobbyBtnController;
    private ColorController colorController;

    public int viewID;

    private void Awake()
    {
        lobbyBtnController = FindObjectOfType<LobbyBtnController>();
        colorController = FindObjectOfType<ColorController>();
        TryGetComponent(out playerColor);
    }

    private void Start()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            if (PhotonNetwork.PlayerList[i].NickName == PV.Controller.NickName)
            {
                viewID = i;
            }
        }

        if (PV.IsMine)
        {
            PV.RPC("Set", RpcTarget.AllBuffered, viewID);
        }
    }

    [PunRPC]
    private void Set(int id)
    {
        transform.SetParent(GameObject.Find("Sort").transform);
        GetComponentInChildren<Text>().text = PV.Controller.NickName;

        lobbyBtnController.playerColor[id] = playerColor;
        colorController.playerColor[id] = playerColor;

        colorController.DefaultColor(id);
    }
}
