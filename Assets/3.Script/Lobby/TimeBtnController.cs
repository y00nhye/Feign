using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TimeBtnController : MonoBehaviourPun
{
    [Header("[Time Text UI]")]
    [SerializeField] Text voteTimeTxt;
    [SerializeField] Text rolePlayTimeTxt;

    [Header("[Current Time (Set)]")]
    public int voteTimeCurrent;
    public int rolePlayTimeCurrent;

    //타이머 기본값
    private int voteTimeDefault = 180;
    private int rolePlayTimeDefault = 30;

    //타이머 변동 수치값 변수
    private int voteTimeValue = 60;
    private int rolePlayTimeValue = 10;

    private PhotonView PV;

    private void Awake()
    {
        PV = GameObject.Find("LobbyManager").GetPhotonView();
    }
    private void Start()
    {
        voteTimeCurrent = voteTimeDefault;
        rolePlayTimeCurrent = rolePlayTimeDefault;
    }
    public void VoteTimeUp_pv()
    {
        photonView.RPC("VoteTimeUp", RpcTarget.AllBuffered);
    }
    public void VoteTimeDown_pv()
    {
        PV.RPC("VoteTimeDown", RpcTarget.AllBuffered);
    }
    public void RolePlayTimeUp_pv()
    {
        PV.RPC("RolePlayTimeUp", RpcTarget.AllBuffered);
    }
    public void RolePlayTimeDown_pv()
    {
        PV.RPC("RolePlayTimeDown", RpcTarget.AllBuffered);
    }
    [PunRPC]
    private void VoteTimeUp()
    {
        voteTimeCurrent += voteTimeValue;

        if(voteTimeCurrent > 300)
        {
            voteTimeCurrent = 300;
        }

        voteTimeTxt.text = "" + voteTimeCurrent;
    }
    [PunRPC]
    private void VoteTimeDown()
    {
        voteTimeCurrent -= voteTimeValue;

        if (voteTimeCurrent < 60)
        {
            voteTimeCurrent = 60;
        }

        voteTimeTxt.text = "" + voteTimeCurrent;
    }
    [PunRPC]
    private void RolePlayTimeUp()
    {
        rolePlayTimeCurrent += rolePlayTimeValue;

        if (rolePlayTimeCurrent > 60)
        {
            rolePlayTimeCurrent = 60;
        }

        rolePlayTimeTxt.text = "" + rolePlayTimeCurrent;
    }
    [PunRPC]
    private void RolePlayTimeDown()
    {
        rolePlayTimeCurrent -= rolePlayTimeValue;

        if (rolePlayTimeCurrent < 10)
        {
            rolePlayTimeCurrent = 10;
        }

        rolePlayTimeTxt.text = "" + rolePlayTimeCurrent;
    }
}
