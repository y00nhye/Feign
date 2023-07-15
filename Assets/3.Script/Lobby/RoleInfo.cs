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
    public int roleSerialNum;
    public bool isImposter = false;
    public bool isNeutral = false;

    private PhotonView PV;

    private void Awake()
    {
        roleController = FindObjectOfType<RoleController>();
        PV = GameObject.Find("LobbyManager").GetPhotonView();
    }
    private void Start()
    {
        transform.localScale = Vector3.one;
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

    //역할 체크
    public void RoleNumCheck()
    {
        if(roleData.roleName == "의사")
        {
            num = roleController.doctorNum;
            roleSerialNum = 1;
        }
        else if (roleData.roleName == "정신병자")
        {
            num = roleController.psychopathNum;
            roleSerialNum = 2;
        }
        else if (roleData.roleName == "청소부")
        {
            num = roleController.cleanerNum;
            roleSerialNum = 3;
        }
        else if (roleData.roleName == "페인터")
        {
            num = roleController.painterNum;
            roleSerialNum = 4;
        }
        else if (roleData.roleName == "경찰" && !isImposter)
        {
            num = roleController.cPoliceOfficerNum;
            roleSerialNum = 5;
        }
        else if (roleData.roleName == "경찰" && isImposter)
        {
            num = roleController.iPoliceOfficerNum;
            roleSerialNum = 6;
        }
        else if (roleData.roleName == "조사관" && !isImposter)
        {
            num = roleController.cAgentNum;
            roleSerialNum = 7;
        }
        else if (roleData.roleName == "조사관" && isImposter)
        {
            num = roleController.iAgentNum;
            roleSerialNum = 8;
        }
        else if (roleData.roleName == "연쇄 살인마")
        {
            num = roleController.serialKillerNum;
            roleSerialNum = 9;
        }
        else if (roleData.roleName == "도둑")
        {
            num = roleController.thiefNum;
            roleSerialNum = 10;
        }
        else if (roleData.roleName == "무작위" && !isImposter && !isNeutral)
        {
            num = roleController.cRandomNum;
            roleSerialNum = 11;
        }
        else if (roleData.roleName == "무작위" && isImposter && !isNeutral)
        {
            num = roleController.iRandomNum;
            roleSerialNum = 12;
        }
        else if (roleData.roleName == "무작위" && !isImposter && isNeutral)
        {
            num = roleController.nRandomNum;
            roleSerialNum = 13;
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
