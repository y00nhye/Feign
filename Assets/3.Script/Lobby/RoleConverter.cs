using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoleConverter : MonoBehaviour
{
    private string roleKey;
    private string receiver;

    private PhotonView PV;

    private void Awake()
    {
        PV = GameObject.Find("LobbyManager").GetPhotonView();
    }
    public void RoleConvert()
    {
        for (int i = 0; i < GameManager.instance.roles.Count; i++)
        {
            if (roleKey == null)
            {
                roleKey += GameManager.instance.shuffleRoles[i].roleSerialNum.ToString();
            }
            else
            {
                roleKey += "," + GameManager.instance.shuffleRoles[i].roleSerialNum.ToString();
            }
        }

        ShareKey_pv();
    }

    public void ShareKey_pv()
    {
        PV.RPC("ShareKey", RpcTarget.AllBuffered, roleKey);
        Debug.Log(roleKey);
    }

    [PunRPC]
    private void ShareKey(string key)
    {
        receiver = key;
    }
}
