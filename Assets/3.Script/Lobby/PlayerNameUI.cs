using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerNameUI : MonoBehaviourPunCallbacks
{
    private PhotonView PV;
    private RectTransform pos;

    private ColorController colorController;
    private Text myNickName;

    private void Awake()
    {
        TryGetComponent(out PV);
        TryGetComponent(out pos);

        colorController = FindObjectOfType<ColorController>();
        myNickName = GetComponentInChildren<Text>();
    }
    private void Start()
    {
        FindObjectOfType<LobbyBtnController>().playerColor = GetComponent<Image>();
        FindObjectOfType<ColorController>().playerColor = GetComponent<Image>();
    }
    private void Update()
    {
        if (PV.IsMine)
        {
            PV.RPC("Set", RpcTarget.All);
        }
    }

    [PunRPC]
    void Set()
    {
        myNickName.text = PhotonNetwork.LocalPlayer.NickName;
        colorController.DefaultColor();

        pos.SetParent(GameObject.Find("Sort").transform);
    }
}
