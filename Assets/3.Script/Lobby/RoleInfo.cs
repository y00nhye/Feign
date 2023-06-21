using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleInfo : MonoBehaviour
{
    public RoleData roleData; //RoleController���� �� ����
    public Color roleColor;
    public int num;

    [Header("Role UI")]
    [SerializeField] Image roleBackground;
    [SerializeField] Image roleImg;
    [SerializeField] Text roleName;
    [SerializeField] Text roleNum;

    private void Update()
    {
        
    }

    public void SetInfo()
    {
        roleBackground.color = roleColor;
        roleImg.sprite = roleData.roleImg;
        roleName.text = roleData.roleName;       
    }
}
