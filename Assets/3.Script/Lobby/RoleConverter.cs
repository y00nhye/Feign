using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoleConverter : MonoBehaviour
{
    public string roleKey = null;

    private PhotonView PV;

    public bool isFinish = false;

    private void Awake()
    {
        TryGetComponent(out PV);
    }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            RoleConverttoString();
        }
    }

    public void RoleConverttoString()
    {
        GameManager.instance.RoleShuffle();

        string key = null;
        
        for (int i = 0; i < GameManager.instance.roles.Count; i++)
        {
            if (i == 0)
            {
                key += GameManager.instance.shuffleRoles[i].roleSerialNum.ToString();
            }
            else
            {
                key += "," + GameManager.instance.shuffleRoles[i].roleSerialNum.ToString();
            }
        }

        PV.RPC("Send", RpcTarget.AllBuffered, key);
    }
    
    [PunRPC]
    void Send(string key)
    {
        roleKey = key;
        RoleConvertertoInt();
    }

    public void RoleConvertertoInt()
    {
        string[] serialNum_s = roleKey.Split(',');
        int[] serialNum_i = new int[serialNum_s.Length];

        for (int i = 0; i < serialNum_s.Length; i++)
        {
            serialNum_i[i] = int.Parse(serialNum_s[i]);
        }

        RoleConvertertoRole(serialNum_i);
    }
    public void RoleConvertertoRole(int[] serialNum)
    {
        GameManager.instance.shuffleRoles = new Role[serialNum.Length];
        List<int> useNum = new List<int>();
        bool isUse = false;

        for (int i = 0; i < GameManager.instance.roles.Count; i++)
        {
            for (int j = 0; j < serialNum.Length; j++)
            {
                if (GameManager.instance.roles[i].roleSerialNum == serialNum[j])
                {
                    for (int k = 0; k < useNum.Count; k++)
                    {
                        if (useNum[k] == j)
                        {
                            isUse = true;
                            break;
                        }
                        else
                        {
                            isUse = false;
                        }
                    }
                    if (!isUse)
                    {
                        GameManager.instance.shuffleRoles[j] = GameManager.instance.roles[i];
                        useNum.Add(j);
                        break;
                    }
                }
            }
        }

        isFinish = true;
    }
}
