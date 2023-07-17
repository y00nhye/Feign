using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LobbyBtnController : MonoBehaviour
{
    [Header("[UI]")]
    [SerializeField] GameObject lobbyMenuUI;
    [SerializeField] GameObject roomCreateUI;
    [SerializeField] GameObject roomEnterUI;
    [SerializeField] GameObject nicknameSettingUI;
    [SerializeField] GameObject createRoomNameUI;
    [SerializeField] GameObject enterRoomNameUI;
    [SerializeField] GameObject optionUI;
    [SerializeField] GameObject numErrorPopup;
    [SerializeField] GameObject imErrorPopup;
    [SerializeField] Button startBtn;

    [Header("[RoleData]")]
    [SerializeField] RoleData[] citizen;
    [SerializeField] RoleData[] imposter;
    [SerializeField] RoleData[] neutral;

    private int randNumC;
    private int randNumI;
    private int randNumN;

    private GameObject errorPopup;

    [Header("[Player Name]")]
    [SerializeField] Text playerNameInput;
    public string playerName { get; private set; }

    [Header("[Player Color (set)]")]
    public Image[] playerColor;

    private PhotonView PV;

    private int loadCheck;
    private bool isStart;

    private List<RoleInfo> roles = new List<RoleInfo>();

    private void Awake()
    {
        PV = GameObject.Find("LobbyManager").GetPhotonView();
    }
    private void Start()
    {
        playerColor = new Image[8];
    }
    private void OnEnable()
    {
        loadCheck = 0;
        isStart = false;

    }
    private void Update()
    {
        if (isStart)
        {
            isStart = false;

            if (PhotonNetwork.IsMasterClient)
            {
                randNumC = Random.Range(0, citizen.Length);
                randNumI = Random.Range(0, imposter.Length);
                randNumN = Random.Range(0, neutral.Length);

                PV.RPC("RandNumSet", RpcTarget.AllBuffered, randNumC, randNumI, randNumN);
            }

            SetRole();
        }

        if (PhotonNetwork.InRoom)
        {
            if (loadCheck == PhotonNetwork.CurrentRoom.PlayerCount)
            {
                loadCheck = 0;
                PhotonNetwork.LoadLevel("Stage");
            }
        }
    }
    [PunRPC]
    private void RandNumSet(int rnd1, int rnd2, int rnd3)
    {
        randNumC = rnd1;
        randNumI = rnd2;
        randNumN = rnd3;
    }
    public void NicknameSet()
    {
        if (playerNameInput.text.Equals(string.Empty))
        {
            playerNameInput.text = "이름을 입력해 주세요!!";
            return;
        }

        playerName = playerNameInput.text;

        nicknameSettingUI.SetActive(false);
        lobbyMenuUI.SetActive(true);
    }
    public void RoomNameCreate()
    {
        lobbyMenuUI.SetActive(false);
        createRoomNameUI.SetActive(true);
    }
    public void RoomCreateOrEnter()
    {
        createRoomNameUI.SetActive(false);
        enterRoomNameUI.SetActive(false);
        lobbyMenuUI.SetActive(false);
        roomCreateUI.SetActive(true);
    }
    public void RoomEnter()
    {
        lobbyMenuUI.SetActive(false);
        enterRoomNameUI.SetActive(true);
    }
    public void OptionEnter()
    {
        optionUI.SetActive(true);
    }
    public void OptionExit()
    {
        optionUI.SetActive(false);
    }
    public void Back()
    {
        createRoomNameUI.SetActive(false);
        enterRoomNameUI.SetActive(false);
        lobbyMenuUI.SetActive(true);

    }
    public void GameExit()
    {
        Application.Quit();
    }

    public void MasterSet()
    {
        startBtn.interactable = true;
    }

    public void GameStart_pv()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount != FindObjectOfType<RoleController>().totalNum || FindObjectOfType<RoleController>().totalNum <= 2)
        {
            errorPopup = numErrorPopup;
            
            StartCoroutine(Error_co());
            return;
        }
        else if (FindObjectOfType<RoleController>().citizenNum <= FindObjectOfType<RoleController>().imposterNum)
        {
            errorPopup = imErrorPopup;
            
            StartCoroutine(Error_co());
            return;
        }

        PV.RPC("GameStart", RpcTarget.AllBuffered, true);
    }

    IEnumerator Error_co()
    {
        errorPopup.SetActive(true);

        yield return new WaitForSeconds(3f);

        errorPopup.SetActive(false);
    }

    [PunRPC]
    private void GameStart(bool start)
    {
        isStart = start;
    }
    [PunRPC]
    private void LoadCheck()
    {
        loadCheck++;
    }
    private void SetRole()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Role").Length; i++)
        {
            if (GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().roleData.roleName == "무작위")
            {
                if (GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().isImposter)
                {
                    GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().roleData = imposter[randNumI];
                    GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().roleSerialNum = GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().roleData.SerialNumI;
                }
                else if (GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().isNeutral)
                {
                    GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().roleData = neutral[randNumN];
                    GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().roleSerialNum = GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().roleData.SerialNumN;
                }
                else
                {
                    GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().roleData = citizen[randNumC];
                    GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().roleSerialNum = GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().roleData.SerialNumC;
                }
            }

            roles.Add(GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>());
        }

        SetInfo();
    }
    private void SetInfo()
    {
        int citizenCnt = 0;
        int imposterCnt = 0;
        int neutralCnt = 0;

        List<Role> citizenRole = new List<Role>();
        List<Role> imposterRole = new List<Role>();
        List<Role> neutralRole = new List<Role>();

        //roleInfo 담기
        for (int i = 0; i < roles.Count; i++)
        {
            Role role = new Role(roles[i].roleData, roles[i].num, roles[i].roleColor, roles[i].isImposter, roles[i].isNeutral, roles[i].roleSerialNum);

            if (role.isImposter)
            {
                imposterRole.Add(role);

                imposterCnt += role.roleCnt;
            }
            else if (role.isNeutral)
            {
                neutralRole.Add(role);

                neutralCnt += role.roleCnt;
            }
            else
            {
                citizenRole.Add(role);

                citizenCnt += role.roleCnt;
            }
        }

        GameManager.instance.roles = citizenRole;

        for (int i = 0; i < imposterRole.Count; i++)
        {
            GameManager.instance.roles.Add(imposterRole[i]);
        }
        for (int i = 0; i < neutralRole.Count; i++)
        {
            GameManager.instance.roles.Add(neutralRole[i]);
        }

        GameManager.instance.citizenNum = citizenCnt;
        GameManager.instance.imposterNum = imposterCnt;
        GameManager.instance.neutralNum = neutralCnt;

        GameManager.instance.totalRoleNum = citizenCnt + imposterCnt + neutralCnt;

        GameManager.instance.currentPlayer = PhotonNetwork.CurrentRoom.PlayerCount;

        //Time 담기
        GameManager.instance.voteTime = FindObjectOfType<TimeBtnController>().voteTimeCurrent;
        GameManager.instance.rolePlayTime = FindObjectOfType<TimeBtnController>().rolePlayTimeCurrent;

        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            GameManager.instance.myColor[i] = playerColor[i].color;
        }

        PV.RPC("LoadCheck", RpcTarget.AllBuffered);
    }

    public void RoomCreateExit()
    {
        lobbyMenuUI.SetActive(true);
        roomCreateUI.SetActive(false);

        FindObjectOfType<PunManager>().LeaveRoom();
    }
}
