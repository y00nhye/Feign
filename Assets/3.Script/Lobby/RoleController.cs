using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RoleController : MonoBehaviourPunCallbacks
{
    [Header("[RoleList UI]")]
    [SerializeField] GameObject citizenUI;
    [SerializeField] GameObject imposterUI;
    [SerializeField] GameObject neutralUI;

    [Header("[RoleList Txt]")]
    [SerializeField] Text citizenNumTxt;
    [SerializeField] Text imposterNumTxt;
    [SerializeField] Text neutralNumTxt;
    [SerializeField] Text totalNumTxt;

    [Header("[RoleList UI Prebs]")]
    [SerializeField] GameObject roleListPrebs;

    [Header("0Doc 1Psy 2Clean 3Paint 4Police 5Agent 6Killer 7Thief 8Ran")]
    [SerializeField] RoleData[] roleDatas;

    //������Ʈ ������ ���� ����
    private List<GameObject> citizen = new List<GameObject>();
    private List<GameObject> imposter = new List<GameObject>();
    private List<GameObject> neutral = new List<GameObject>();

    //���� �� ���� ����
    private int citizenNum = 0;
    private int imposterNum = 0;
    private int neutralNum = 0;
    private int totalNum = 0;
    public int doctorNum { get; private set; }
    public int psychopathNum { get; private set; }
    public int cleanerNum { get; private set; }
    public int painterNum { get; private set; }
    public int cPoliceOfficerNum { get; private set; }
    public int iPoliceOfficerNum { get; private set; }
    public int cAgentNum { get; private set; }
    public int iAgentNum { get; private set; }
    public int serialKillerNum { get; private set; }
    public int thiefNum { get; private set; }
    public int cRandomNum { get; private set; }
    public int iRandomNum { get; private set; }
    public int nRandomNum { get; private set; }

    private Vector3 PlusUIPos = new Vector3(0, 120, 0); //���� UI ��ġ��, ���� UI ��ġ�� localPosition ���� �ޱ�

    [Header("[Photon View]")]
    public PhotonView PV;

    private void Start()
    {
        doctorNum = 0;
        psychopathNum = 0;
        cleanerNum = 0;
        painterNum = 0;
        cPoliceOfficerNum = 0;
        iPoliceOfficerNum = 0;
        cAgentNum = 0;
        iAgentNum = 0;
        serialKillerNum = 0;
        thiefNum = 0;
        cRandomNum = 0;
        iRandomNum = 0;
        nRandomNum = 0;
    }
    private void Update()
    {
        citizenNumTxt.text = "" + citizenNum; //�ù� ���� ��
        imposterNumTxt.text = "" + imposterNum; //�������� ���� ��
        neutralNumTxt.text = "" + neutralNum; //�߸� ���� ��

        //��ü ���� ��
        totalNum = citizenNum + imposterNum + neutralNum;
        totalNumTxt.text = "" + totalNum;
    }

    private GameObject CitizenRoleSet() //�ù� ����
    {
        imposterUI.transform.position -= PlusUIPos;
        neutralUI.transform.position -= PlusUIPos;

        GameObject role = Instantiate(roleListPrebs);
        role.transform.SetParent(GameObject.Find("Citizen").transform);

        citizen.Add(role);

        role.transform.localPosition = new Vector3(0, 20, 0) - PlusUIPos * citizen.Count;
        role.GetComponent<RoleInfo>().roleColor = citizenUI.GetComponent<Image>().color;

        return role;
    }
    private GameObject ImposterRoleSet() //�������� ����
    {
        neutralUI.transform.position -= PlusUIPos;

        GameObject role = Instantiate(roleListPrebs);
        role.transform.SetParent(GameObject.Find("Imposter").transform);

        imposter.Add(role);

        role.transform.localPosition = new Vector3(0, 20, 0) - PlusUIPos * imposter.Count;
        role.GetComponent<RoleInfo>().roleColor = imposterUI.GetComponent<Image>().color;
        role.GetComponent<RoleInfo>().isImposter = true;

        return role;
    }
    private GameObject NeutralRoleSet() //�߸� ����
    {
        GameObject role = Instantiate(roleListPrebs);
        role.transform.SetParent(GameObject.Find("Neutral").transform);

        neutral.Add(role);

        role.transform.localPosition = new Vector3(0, 20, 0) - PlusUIPos * neutral.Count;
        role.GetComponent<RoleInfo>().roleColor = neutralUI.GetComponent<Image>().color;
        role.GetComponent<RoleInfo>().isNeutral = true;

        return role;
    }

    public void DoctorPlus_pv()
    {
        PV.RPC("DoctorPlus", RpcTarget.AllBuffered);
    }
    public void PsychopathPlus_pv()
    {
        PV.RPC("PsychopathPlus", RpcTarget.AllBuffered);
    }
    public void CleanerPlus_pv()
    {
        PV.RPC("CleanerPlus", RpcTarget.AllBuffered);
    }
    public void PainterPlus_pv()
    {
        PV.RPC("PainterPlus", RpcTarget.AllBuffered);
    }
    public void CPoliceOfficerPlus_pv()
    {
        PV.RPC("CPoliceOfficerPlus", RpcTarget.AllBuffered);
    }
    public void IPoliceOfficerPlus_pv()
    {
        PV.RPC("IPoliceOfficerPlus", RpcTarget.AllBuffered);
    }
    public void CAgentPlus_pv()
    {
        PV.RPC("CAgentPlus", RpcTarget.AllBuffered);
    }
    public void IAgentPlus_pv()
    {
        PV.RPC("IAgentPlus", RpcTarget.AllBuffered);
    }
    public void SerialKillerPlus_pv()
    {
        PV.RPC("SerialKillerPlus", RpcTarget.AllBuffered);
    }
    public void ThiefPlus_pv()
    {
        PV.RPC("ThiefPlus", RpcTarget.AllBuffered);
    }
    public void CRandomPlus_pv()
    {
        PV.RPC("CRandomPlus", RpcTarget.AllBuffered);
    }
    public void IRandomPlus_pv()
    {
        PV.RPC("IRandomPlus", RpcTarget.AllBuffered);
    }
    public void NRandomPlus_pv()
    {
        PV.RPC("NRandomPlus", RpcTarget.AllBuffered);
    }

    //���� �߰� ��ư �̺�Ʈ 13��
    [PunRPC]
    private void DoctorPlus()
    {
        doctorNum++;
        citizenNum++;

        if (doctorNum < 2)
        {
            RoleInfo roleInfo = CitizenRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[0];
            roleInfo.SetInfo();
        }
    }
    [PunRPC]
    private void PsychopathPlus()
    {
        psychopathNum++;
        citizenNum++;

        if (psychopathNum < 2)
        {
            RoleInfo roleInfo = CitizenRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[1];
            roleInfo.SetInfo();
        }
    }
    [PunRPC]
    private void CleanerPlus()
    {
        cleanerNum++;
        imposterNum++;

        if (cleanerNum < 2)
        {
            RoleInfo roleInfo = ImposterRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[2];
            roleInfo.SetInfo();
        }
    }
    [PunRPC]
    private void PainterPlus()
    {
        painterNum++;
        imposterNum++;

        if (painterNum < 2)
        {
            RoleInfo roleInfo = ImposterRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[3];
            roleInfo.SetInfo();
        }
    }
    [PunRPC]
    private void CPoliceOfficerPlus()
    {
        cPoliceOfficerNum++;
        citizenNum++;

        if (cPoliceOfficerNum < 2)
        {
            RoleInfo roleInfo = CitizenRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[4];
            roleInfo.SetInfo();
        }
    }
    [PunRPC]
    private void IPoliceOfficerPlus()
    {
        iPoliceOfficerNum++;
        imposterNum++;

        if (iPoliceOfficerNum < 2)
        {
            RoleInfo roleInfo = ImposterRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[4];
            roleInfo.SetInfo();
        }
    }
    [PunRPC]
    private void CAgentPlus()
    {
        cAgentNum++;
        citizenNum++;

        if (cAgentNum < 2)
        {
            RoleInfo roleInfo = CitizenRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[5];
            roleInfo.SetInfo();
        }
    }
    [PunRPC]
    private void IAgentPlus()
    {
        iAgentNum++;
        imposterNum++;

        if (iAgentNum < 2)
        {
            RoleInfo roleInfo = ImposterRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[5];
            roleInfo.SetInfo();
        }
    }
    [PunRPC]
    private void SerialKillerPlus()
    {
        serialKillerNum++;
        neutralNum++;

        if (serialKillerNum < 2)
        {
            RoleInfo roleInfo = NeutralRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[6];
            roleInfo.SetInfo();
        }
    }
    [PunRPC]
    private void ThiefPlus()
    {
        thiefNum++;
        neutralNum++;

        if (thiefNum < 2)
        {
            RoleInfo roleInfo = NeutralRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[7];
            roleInfo.SetInfo();
        }
    }
    [PunRPC]
    private void CRandomPlus()
    {
        cRandomNum++;
        citizenNum++;

        if (cRandomNum < 2)
        {
            RoleInfo roleInfo = CitizenRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[8];
            roleInfo.SetInfo();
        }
    }
    [PunRPC]
    private void IRandomPlus()
    {
        iRandomNum++;
        imposterNum++;

        if (iRandomNum < 2)
        {
            RoleInfo roleInfo = ImposterRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[8];
            roleInfo.SetInfo();
        }
    }
    [PunRPC]
    private void NRandomPlus()
    {
        nRandomNum++;
        neutralNum++;

        if (nRandomNum < 2)
        {
            RoleInfo roleInfo = NeutralRoleSet().GetComponent<RoleInfo>();
            roleInfo.roleData = roleDatas[8];
            roleInfo.SetInfo();
        }
    }

    [PunRPC]
    //���� ����, �����տ��� ����
    public void RoleMinus(bool isim, bool isne, string name)
    {
        int re = 0; //������ ���� ����, re ��° �ε������� �ڸ� ����

        if (!isim && !isne) //�ù� ���� ����
        {
            citizenNum--;
            int currentNum; //���� ������ �� üũ

            if(name == "�ǻ�")
            {
                doctorNum--;
                currentNum = doctorNum;
            }
            else if (name == "���ź���")
            {
                psychopathNum--;
                currentNum = psychopathNum;
            }
            else if (name == "����")
            {
                cPoliceOfficerNum--;
                currentNum = cPoliceOfficerNum;
            }
            else if (name == "�����")
            {
                cAgentNum--;
                currentNum = cAgentNum;
            }
            else if (name == "������")
            {
                cRandomNum--;
                currentNum = cRandomNum;
            }
            else
            {
                Debug.Log("There is Not correct roleName");
                return;
            }

            if (currentNum < 1)
            {
                for (int i = 0; i < citizen.Count; i++)
                {
                    if (citizen[i].GetComponent<RoleInfo>().roleData.roleName == name)
                    {
                        Destroy(citizen[i]);
                        citizen.RemoveAt(i);

                        re = i;

                        break;
                    }
                }

                imposterUI.transform.position += PlusUIPos;
                neutralUI.transform.position += PlusUIPos;

                if (citizen.Count > 0)
                {
                    for (int i = re; i < citizen.Count; i++)
                    {
                        citizen[i].transform.localPosition += PlusUIPos;
                    }
                }
            }
        }
        else if (isim) //�������� ���� ����
        {
            imposterNum--;
            int currentNum; //���� ������ �� üũ

            if (name == "û�Һ�")
            {
                cleanerNum--;
                currentNum = cleanerNum;
            }
            else if (name == "������")
            {
                painterNum--;
                currentNum = painterNum;
            }
            else if (name == "����")
            {
                iPoliceOfficerNum--;
                currentNum = iPoliceOfficerNum;
            }
            else if (name == "�����")
            {
                iAgentNum--;
                currentNum = iAgentNum;
            }
            else if (name == "������")
            {
                iRandomNum--;
                currentNum = iRandomNum;
            }
            else
            {
                Debug.Log("There is Not correct roleName");
                return;
            }

            if (currentNum < 1)
            {
                for (int i = 0; i < imposter.Count; i++)
                {
                    if (imposter[i].GetComponent<RoleInfo>().roleData.roleName == name)
                    {
                        Destroy(imposter[i]);
                        imposter.RemoveAt(i);

                        re = i;

                        break;
                    }
                }

                neutralUI.transform.position += PlusUIPos;

                if (imposter.Count > 0)
                {
                    for (int i = re; i < imposter.Count; i++)
                    {
                        imposter[i].transform.localPosition += PlusUIPos;
                    }
                }
            }
        }
        else if (isne) //�߸� ���� ����
        {
            neutralNum--;
            int currentNum; //���� ������ �� üũ

            if (name == "���� ���θ�")
            {
                serialKillerNum--;
                currentNum = serialKillerNum;
            }
            else if (name == "����")
            {
                thiefNum--;
                currentNum = thiefNum;
            }
            else if (name == "������")
            {
                nRandomNum--;
                currentNum = nRandomNum;
            }
            else
            {
                Debug.Log("There is Not correct roleName");
                return;
            }

            if (currentNum < 1)
            {
                for (int i = 0; i < neutral.Count; i++)
                {
                    if (neutral[i].GetComponent<RoleInfo>().roleData.roleName == name)
                    {
                        Destroy(neutral[i]);
                        neutral.RemoveAt(i);

                        re = i;

                        break;
                    }
                }

                if (neutral.Count > 0)
                {
                    for (int i = re; i < neutral.Count; i++)
                    {
                        neutral[i].transform.localPosition += PlusUIPos;
                    }
                }
            }
        }
    }
}
