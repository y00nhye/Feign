using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleController : MonoBehaviour
{
    [Header("RoleList UI")]
    [SerializeField] GameObject citizenUI;
    [SerializeField] GameObject imposterUI;
    [SerializeField] GameObject neutralUI;

    [Header("RoleList Txt")]
    [SerializeField] Text citizenNumTxt;
    [SerializeField] Text imposterNumTxt;
    [SerializeField] Text neutralNumTxt;
    [SerializeField] Text totalNumTxt;

    [Header("RoleList UI Prebs")]
    [SerializeField] GameObject roleListPrebs;

    [Header("Doc > Psy > Clean > Paint > Police > Agent > Killer > Thief")]
    [SerializeField] RoleData[] roleDatas;

    //���� �� ���� ����
    private int citizenNum = 0;
    private int imposterNum = 0;
    private int neutralNum = 0;
    private int totalNum = 0;

    private int doctorNum = 0;
    private int psychopathNum = 0;
    private int cleanerNum = 0;
    private int painterNum = 0;
    private int cPoliceOfficerNum = 0;
    private int iPoliceOfficerNum = 0;
    private int cAgentNum = 0;
    private int iAgentNum = 0;
    private int serialKillerNum = 0;
    private int thiefNum = 0;

    private Vector3 PlusUIPos = new Vector3(0, 120, 0); //���� UI ��ġ��, ���� UI ��ġ�� localPosition ���� �ޱ�

    private void Update()
    {
        citizenNumTxt.text = "" + citizenNum; //�ù� ���� ��
        imposterNumTxt.text = "" + imposterNum; //�������� ���� ��
        neutralNumTxt.text = "" + neutralNum; //�߸� ���� ��

        //��ü ���� ��
        totalNum = citizenNum + imposterNum + neutralNum;
        totalNumTxt.text = "" + totalNum;
    }

    private GameObject CitizenRoleSet()
    {
        citizenNum++;

        imposterUI.transform.position -= PlusUIPos;
        neutralUI.transform.position -= PlusUIPos;

        //!!!��ø �����ϱ� ����!!!

        GameObject role = Instantiate(roleListPrebs);
        role.transform.SetParent(GameObject.Find("Citizen").transform);

        role.transform.localPosition = new Vector3(0, 20, 0) - PlusUIPos * citizenNum;
        role.GetComponent<RoleInfo>().roleColor = citizenUI.GetComponent<Image>().color;

        return role;
    }
    private GameObject ImposterRoleSet()
    {
        imposterNum++;

        neutralUI.transform.position -= PlusUIPos;

        //!!!��ø �����ϱ� ����!!!

        GameObject role = Instantiate(roleListPrebs);
        role.transform.SetParent(GameObject.Find("Imposter").transform);

        role.transform.localPosition = new Vector3(0, 20, 0) - PlusUIPos * imposterNum;
        role.GetComponent<RoleInfo>().roleColor = imposterUI.GetComponent<Image>().color;

        return role;
    }
    private GameObject NeutralRoleSet()
    {
        neutralNum++;

        //!!!��ø �����ϱ� ����!!!

        GameObject role = Instantiate(roleListPrebs);
        role.transform.SetParent(GameObject.Find("Neutral").transform);

        role.transform.localPosition = new Vector3(0, 20, 0) - PlusUIPos * neutralNum;
        role.GetComponent<RoleInfo>().roleColor = neutralUI.GetComponent<Image>().color;

        return role;
    }

    public void DoctorPlus()
    {
        doctorNum++;

        if (doctorNum < 2)
        {
            RoleInfo roleInfo = CitizenRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[0];
            roleInfo.SetInfo();
        }
    }
    public void PsychopathPlus()
    {
        psychopathNum++;

        if (psychopathNum < 2)
        {
            RoleInfo roleInfo = CitizenRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[1];
            roleInfo.SetInfo();
        }

    }
    public void CleanerPlus()
    {
        cleanerNum++;

        if (cleanerNum < 2)
        {
            RoleInfo roleInfo = ImposterRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[2];
            roleInfo.SetInfo();
        }

    }
    public void PainterPlus()
    {
        painterNum++;

        if (painterNum < 2)
        {
            RoleInfo roleInfo = ImposterRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[3];
            roleInfo.SetInfo();
        }

    }
    public void CPoliceOfficerPlus()
    {
        cPoliceOfficerNum++;

        if (cPoliceOfficerNum < 2)
        {
            RoleInfo roleInfo = CitizenRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[4];
            roleInfo.SetInfo();
        }

    }
    public void IPoliceOfficerPlus()
    {
        iPoliceOfficerNum++;

        if (iPoliceOfficerNum < 2)
        {
            RoleInfo roleInfo = ImposterRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[4];
            roleInfo.SetInfo();
        }

    }
    public void CAgentPlus()
    {
        cAgentNum++;

        if (cAgentNum < 2)
        {
            RoleInfo roleInfo = CitizenRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[5];
            roleInfo.SetInfo();
        }

    }
    public void IAgentPlus()
    {
        iAgentNum++;

        if (iAgentNum < 2)
        {
            RoleInfo roleInfo = ImposterRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[5];
            roleInfo.SetInfo();
        }

    }
    public void SerialKillerPlus()
    {
        serialKillerNum++;

        if (serialKillerNum < 2)
        {
            RoleInfo roleInfo = NeutralRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[6];
            roleInfo.SetInfo();
        }

    }
    public void ThiefPlus()
    {
        thiefNum++;

        if (thiefNum < 2)
        {
            RoleInfo roleInfo = NeutralRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[7];
            roleInfo.SetInfo();
        }

    }
}
