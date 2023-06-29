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

    public int viewID;

    private void Awake()
    {
        lobbyBtnController = FindObjectOfType<LobbyBtnController>();
        colorController = FindObjectOfType<ColorController>();

        viewID = ((PV.ViewID - 1) / 1000) - 1;

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
        colorController.playerColor[((PV.ViewID - 1) / 1000) - 1] = GetComponent<Image>();

        colorController.DefaultColor_pv(((PV.ViewID - 1) / 1000) - 1);
    }
}
