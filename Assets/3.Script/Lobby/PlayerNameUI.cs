using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerNameUI : MonoBehaviourPun
{
    public int viewID;

    private Image playerColor;

    private LobbyBtnController lobbyBtnController;
    private ColorController colorController;

    private void Awake()
    {
        lobbyBtnController = FindObjectOfType<LobbyBtnController>();
        colorController = FindObjectOfType<ColorController>();
        TryGetComponent(out playerColor);
    }

    private void OnEnable()
    {
        if (viewID == (int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"])
        {
            GetComponentInChildren<Text>().text = PhotonNetwork.LocalPlayer.NickName;
        }

    }

    private void Set(int id)
    {
        //GetComponentInChildren<Text>().text = PV.Controller.NickName;

        lobbyBtnController.playerColor[id] = playerColor;
        colorController.playerColor[id] = playerColor;

        // PV.RPC("ColorSet", RpcTarget.AllBuffered, id);
    }

    [PunRPC]
    private void ColorSet(int myNum)
    {
        //colorController.DefaultColor();
    }
}
