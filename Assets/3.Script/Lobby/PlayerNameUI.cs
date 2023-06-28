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

    private void Awake()
    {
        TryGetComponent(out PV);
        TryGetComponent(out pos);
    }
    private void Start()
    {
        if (PV.IsMine)
        {
            GetComponentInChildren<Text>().text = PhotonNetwork.LocalPlayer.NickName;
            FindObjectOfType<LobbyBtnController>().playerColor = GetComponent<Image>();
            FindObjectOfType<ColorController>().playerColor = GetComponent<Image>();
            FindObjectOfType<ColorController>().DefaultColor();

            PV.RPC("Pos", RpcTarget.All);
        }
    }
    private void Update()
    {

    }

    [PunRPC]
    void Pos()
    {
        pos.SetParent(GameObject.Find("Sort").transform);
    }
}
