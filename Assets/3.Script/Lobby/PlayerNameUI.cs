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

    private Text myNickName;

    private void Awake()
    {
        TryGetComponent(out PV);
        TryGetComponent(out pos);

        myNickName = GetComponentInChildren<Text>();
    }
    private void Start()
    {
        FindObjectOfType<LobbyBtnController>().playerColor = GetComponent<Image>();
        FindObjectOfType<ColorController>().playerColor = GetComponent<Image>();

        if (PV.IsMine)
        {
            PV.RPC("Set", RpcTarget.AllBuffered);
        }
    }
    [PunRPC]
    void Set()
    {
        myNickName.text = PhotonNetwork.LocalPlayer.NickName;
        pos.SetParent(GameObject.Find("Sort").transform);
        FindObjectOfType<ColorController>().DefaultColor();
    }
}
