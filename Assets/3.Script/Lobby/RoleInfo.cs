using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RoleInfo : MonoBehaviour
{
    private RoleController roleController;

    [Header("[Role UI]")]
    [SerializeField] Image roleBackground;
    [SerializeField] Image roleImg;
    [SerializeField] Text roleName;
    [SerializeField] Text roleNum;

    [Header("[RoleController Data (Set)]")]
    public RoleData roleData;
    public Color roleColor;
    public int num = 0;
    public bool isImposter = false;
    public bool isNeutral = false;

    private PhotonView PV;

    private void Awake()
    {
        roleController = FindObjectOfType<RoleController>();
        PV = GameObject.Find("LobbyManager").GetPhotonView();
    }
    private void Update()
    {
        RoleNumCheck();
        
        if(num > 1)
        {
            roleNum.text = "x" + num;
        }
        else
        {
            roleNum.text = "";
        }
    }

    //���� üũ
    public void RoleNumCheck()
    {
        if(roleData.roleName == "�ǻ�")
        {
            num = roleController.doctorNum;
        }
        else if (roleData.roleName == "���ź���")
        {
            num = roleController.psychopathNum;
        }
        else if (roleData.roleName == "û�Һ�")
        {
            num = roleController.cleanerNum;
        }
        else if (roleData.roleName == "������")
        {
            num = roleController.painterNum;
        }
        else if (roleData.roleName == "����" && !isImposter)
        {
            num = roleController.cPoliceOfficerNum;
        }
        else if (roleData.roleName == "����" && isImposter)
        {
            num = roleController.iPoliceOfficerNum;
        }
        else if (roleData.roleName == "�����" && !isImposter)
        {
            num = roleController.cAgentNum;
        }
        else if (roleData.roleName == "�����" && isImposter)
        {
            num = roleController.iAgentNum;
        }
        else if (roleData.roleName == "���� ���θ�")
        {
            num = roleController.serialKillerNum;
        }
        else if (roleData.roleName == "����")
        {
            num = roleController.thiefNum;
        }
        else if (roleData.roleName == "������" && !isImposter && !isNeutral)
        {
            num = roleController.cRandomNum;
        }
        else if (roleData.roleName == "������" && isImposter && !isNeutral)
        {
            num = roleController.iRandomNum;
        }
        else if (roleData.roleName == "������" && !isImposter && isNeutral)
        {
            num = roleController.nRandomNum;
        }
    }

    public void SetInfo()
    {
        roleBackground.color = roleColor;
        roleImg.sprite = roleData.roleImg;
        roleName.text = roleData.roleName; 
    }
    public void Minus_pv()
    {
        PV.RPC("RoleMinus", RpcTarget.AllBuffered, isImposter, isNeutral, roleData.roleName);
    }
}
