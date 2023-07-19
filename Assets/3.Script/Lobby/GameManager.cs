using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[System.Serializable]
public class Role
{
    public RoleData roleData;
    public int roleCnt = 0;
    public int roleSerialNum;
    public Color roleColor;

    public bool isImposter;
    public bool isNeutral;

    public Role(RoleData roleData, int roleCnt, Color roleColor, bool isImposter, bool isNeutral, int roleSerialNum)
    {
        this.roleData = roleData;
        this.roleCnt = roleCnt;
        this.roleColor = roleColor;
        this.isImposter = isImposter;
        this.isNeutral = isNeutral;
        this.roleSerialNum = roleSerialNum;
    }
}
public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("GameManager already load.");
            Destroy(gameObject);
        }
    }
    #endregion
    public GameObject[] playerPrefs;

    //역할 정보 담는 변수
    public List<Role> roles = new List<Role>();
    public Role[] shuffleRoles;

    //투표, 역할 시간 담는 변수
    public int voteTime;
    public int rolePlayTime;

    //플레이어 정보 담는 변수, 플레이어 생성 되면 제작
    public Color[] myColor;
    public int[] myColorNum;

    //역할 수 담는 변수
    public int citizenNum;
    public int imposterNum;
    public int neutralNum;

    public int totalRoleNum;

    public int currentPlayer;

    public TimeManager timeManager;

    public string nickName;

    private void Start()
    {
        playerPrefs = new GameObject[8];
        myColor = new Color[8];
        myColorNum = new int[8];
    }
    public bool GameOverCheck()
    {
        if (imposterNum == 0 && neutralNum == 0)
        {
            timeManager.isVote = false;
            timeManager.isNight = false;
        
            timeManager.isTimeChange = true;
            timeManager.isLoading = true;
        
            GameObject.Find("EndingUI").transform.GetChild(0).gameObject.SetActive(true);
            FindObjectOfType<EndingUISetting>().WinnerSet(1);
        
            return true;
        }
        else if (citizenNum + neutralNum <= imposterNum)
        {
            timeManager.isVote = false;
            timeManager.isNight = false;
        
            timeManager.isTimeChange = true;
            timeManager.isLoading = true;
        
            GameObject.Find("EndingUI").transform.GetChild(0).gameObject.SetActive(true);
            FindObjectOfType<EndingUISetting>().WinnerSet(2);
        
            return true;
        }
        else if (citizenNum + imposterNum == 0 && neutralNum > 0)
        {
            timeManager.isVote = false;
            timeManager.isNight = false;
        
            timeManager.isTimeChange = true;
            timeManager.isLoading = true;
        
            GameObject.Find("EndingUI").transform.GetChild(0).gameObject.SetActive(true);
            FindObjectOfType<EndingUISetting>().WinnerSet(3);
        
            return true;
        }

        return false;
    }
    public void RoleShuffle()
    {
        shuffleRoles = new Role[totalRoleNum];

        int j = 0;
        for (int i = 0; i < roles.Count; i++)
        {
            Debug.Log(roles[i].roleCnt);

            for(int k = 0; k < roles[i].roleCnt; k++)
            {
                shuffleRoles[j] = roles[i];

                j++;
            }
        }

        int ran1;
        int ran2;

        Role temp;

        for (int i = 0; i < 10; i++)
        {
            ran1 = Random.Range(0, roles.Count);
            ran2 = Random.Range(0, roles.Count);

            temp = shuffleRoles[ran1];
            shuffleRoles[ran1] = shuffleRoles[ran2];
            shuffleRoles[ran2] = temp;
        }
    }

    //셔플은 해놨음, 플레이어 순서대로 셔플된 role 클래스 담으면 됨, 플레이어쪽에서 이미지 설정, 직업 능력 세팅해주기
}
