using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        roleController = FindObjectOfType<RoleController>();
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
        }
        else if (roleData.roleName == "정신병자")
        {
            num = roleController.psychopathNum;
        }
        else if (roleData.roleName == "청소부")
        {
            num = roleController.cleanerNum;
        }
        else if (roleData.roleName == "페인터")
        {
            num = roleController.painterNum;
        }
        else if (roleData.roleName == "경찰" && !isImposter)
        {
            num = roleController.cPoliceOfficerNum;
        }
        else if (roleData.roleName == "경찰" && isImposter)
        {
            num = roleController.iPoliceOfficerNum;
        }
        else if (roleData.roleName == "조사관" && !isImposter)
        {
            num = roleController.cAgentNum;
        }
        else if (roleData.roleName == "조사관" && isImposter)
        {
            num = roleController.iAgentNum;
        }
        else if (roleData.roleName == "연쇄 살인마")
        {
            num = roleController.serialKillerNum;
        }
        else if (roleData.roleName == "도둑")
        {
            num = roleController.thiefNum;
        }
        else if (roleData.roleName == "무작위" && !isImposter && !isNeutral)
        {
            num = roleController.cRandomNum;
        }
        else if (roleData.roleName == "무작위" && isImposter && !isNeutral)
        {
            num = roleController.iRandomNum;
        }
        else if (roleData.roleName == "무작위" && !isImposter && isNeutral)
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
    public void Minus()
    {
        roleController.RoleMinus(GetComponent<RoleInfo>());
    }
}
