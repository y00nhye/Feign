using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleController : MonoBehaviour
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

    //오브젝트 관리를 위한 변수
    private List<GameObject> citizen = new List<GameObject>();
    private List<GameObject> imposter = new List<GameObject>();
    private List<GameObject> neutral = new List<GameObject>();

    //역할 수 세기 변수
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

    private Vector3 PlusUIPos = new Vector3(0, 120, 0); //변경 UI 위치값, 생성 UI 위치는 localPosition 으로 받기

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
        citizenNumTxt.text = "" + citizenNum; //시민 역할 수
        imposterNumTxt.text = "" + imposterNum; //임포스터 역할 수
        neutralNumTxt.text = "" + neutralNum; //중립 역할 수

        //전체 역할 수
        totalNum = citizenNum + imposterNum + neutralNum;
        totalNumTxt.text = "" + totalNum;
    }

    private GameObject CitizenRoleSet() //시민 세팅
    {
        imposterUI.transform.position -= PlusUIPos;
        neutralUI.transform.position -= PlusUIPos;

        //!!!중첩 관리하기 구현!!!

        GameObject role = Instantiate(roleListPrebs);
        role.transform.SetParent(GameObject.Find("Citizen").transform);

        citizen.Add(role);

        role.transform.localPosition = new Vector3(0, 20, 0) - PlusUIPos * citizen.Count;
        role.GetComponent<RoleInfo>().roleColor = citizenUI.GetComponent<Image>().color;

        return role;
    }
    private GameObject ImposterRoleSet() //임포스터 세팅
    {
        neutralUI.transform.position -= PlusUIPos;

        //!!!중첩 관리하기 구현!!!

        GameObject role = Instantiate(roleListPrebs);
        role.transform.SetParent(GameObject.Find("Imposter").transform);

        imposter.Add(role);

        role.transform.localPosition = new Vector3(0, 20, 0) - PlusUIPos * imposter.Count;
        role.GetComponent<RoleInfo>().roleColor = imposterUI.GetComponent<Image>().color;
        role.GetComponent<RoleInfo>().isImposter = true;

        return role;
    }
    private GameObject NeutralRoleSet() //중립 세팅
    {
        //!!!중첩 관리하기 구현!!!

        GameObject role = Instantiate(roleListPrebs);
        role.transform.SetParent(GameObject.Find("Neutral").transform);

        neutral.Add(role);

        role.transform.localPosition = new Vector3(0, 20, 0) - PlusUIPos * neutral.Count;
        role.GetComponent<RoleInfo>().roleColor = neutralUI.GetComponent<Image>().color;
        role.GetComponent<RoleInfo>().isNeutral = true;

        return role;
    }

    //역할 추가 버튼 이벤트 13개
    public void DoctorPlus()
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
    public void PsychopathPlus()
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
    public void CleanerPlus()
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
    public void PainterPlus()
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
    public void CPoliceOfficerPlus()
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
    public void IPoliceOfficerPlus()
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
    public void CAgentPlus()
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
    public void IAgentPlus()
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
    public void SerialKillerPlus()
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
    public void ThiefPlus()
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
    public void CRandomPlus()
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
    public void IRandomPlus()
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
    public void NRandomPlus()
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

    //역할 삭제, 프리팹에서 삭제
    public void RoleMinus(RoleInfo role)
    {
        int re = 0; //정렬을 위한 변수, re 번째 인덱스부터 자리 세팅

        if (role.roleColor == citizenUI.GetComponent<Image>().color) //시민 역할 삭제
        {
            citizenNum--;
            int currentNum; //현재 역할의 수 체크

            if(role.roleData.roleName == "의사")
            {
                doctorNum--;
                currentNum = doctorNum;
            }
            else if (role.roleData.roleName == "정신병자")
            {
                psychopathNum--;
                currentNum = psychopathNum;
            }
            else if (role.roleData.roleName == "경찰")
            {
                cPoliceOfficerNum--;
                currentNum = cPoliceOfficerNum;
            }
            else if (role.roleData.roleName == "조사관")
            {
                cAgentNum--;
                currentNum = cAgentNum;
            }
            else if (role.roleData.roleName == "무작위")
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
                    if (citizen[i] == role.gameObject)
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
        else if (role.roleColor == imposterUI.GetComponent<Image>().color) //임포스터 역할 삭제
        {
            imposterNum--;
            int currentNum; //현재 역할의 수 체크

            if (role.roleData.roleName == "청소부")
            {
                cleanerNum--;
                currentNum = cleanerNum;
            }
            else if (role.roleData.roleName == "페인터")
            {
                painterNum--;
                currentNum = painterNum;
            }
            else if (role.roleData.roleName == "경찰")
            {
                iPoliceOfficerNum--;
                currentNum = iPoliceOfficerNum;
            }
            else if (role.roleData.roleName == "조사관")
            {
                iAgentNum--;
                currentNum = iAgentNum;
            }
            else if (role.roleData.roleName == "무작위")
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
                    if (imposter[i] == role.gameObject)
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
        else if (role.roleColor == neutralUI.GetComponent<Image>().color) //중립 역할 삭제
        {
            neutralNum--;
            int currentNum; //현재 역할의 수 체크

            if (role.roleData.roleName == "연쇄 살인마")
            {
                serialKillerNum--;
                currentNum = serialKillerNum;
            }
            else if (role.roleData.roleName == "도둑")
            {
                thiefNum--;
                currentNum = thiefNum;
            }
            else if (role.roleData.roleName == "무작위")
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
                    if (neutral[i] == role.gameObject)
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
