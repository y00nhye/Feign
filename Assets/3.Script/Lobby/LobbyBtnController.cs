using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyBtnController : MonoBehaviour
{
    [Header("[UI]")]
    [SerializeField] GameObject lobbyMenuUI;
    [SerializeField] GameObject roomCreateUI;
    [SerializeField] GameObject roomEnterUI;
    [SerializeField] GameObject nicknameSettingUI;
    [SerializeField] GameObject createRoomNameUI;
    [SerializeField] GameObject enterRoomNameUI;
    [SerializeField] GameObject errorPopup;
    [SerializeField] Button startBtn;

    [Header("[Player Name]")]
    [SerializeField] Text playerNameInput;
    public string playerName { get; private set; }

    [Header("[Player Color (set)]")]
    public Image[] playerColor;

    private PhotonView PV;

    private void Awake()
    {
        PV = GameObject.Find("LobbyManager").GetPhotonView();
    }
    private void Start()
    {
        playerColor = new Image[8];
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
        if (PhotonNetwork.CurrentRoom.PlayerCount != FindObjectOfType<RoleController>().totalNum || FindObjectOfType<RoleController>().totalNum <= 0)
        {
            StartCoroutine(Error_co());
            return;
        }

        PV.RPC("GameStart", RpcTarget.AllBuffered);
    }

    IEnumerator Error_co()
    {
        errorPopup.SetActive(true);

        yield return new WaitForSeconds(3f);

        errorPopup.SetActive(false);
    }

    [PunRPC]
    private void GameStart()
    {
        int citizenCnt = 0;
        int imposterCnt = 0;
        int neutralCnt = 0;

        List<Role> citizenRole = new List<Role>();
        List<Role> imposterRole = new List<Role>();
        List<Role> neutralRole = new List<Role>();

        //roleInfo 담기
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Role").Length; i++)
        {
            Role role = new Role(GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().roleData, GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().num,
                GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().roleColor, GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().isImposter, GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().isNeutral, GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().roleSerialNum);

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

        if (PhotonNetwork.IsMasterClient)
        {
            GameManager.instance.RoleShuffle();
            FindObjectOfType<RoleConverter>().RoleConvert();
        }

        GameManager.instance.citizenNum = citizenCnt;
        GameManager.instance.imposterNum = imposterCnt;
        GameManager.instance.neutralNum = neutralCnt;

        GameManager.instance.totalRoleNum = citizenCnt + imposterCnt + neutralCnt;

        //Time 담기
        GameManager.instance.voteTime = FindObjectOfType<TimeBtnController>().voteTimeCurrent;
        GameManager.instance.rolePlayTime = FindObjectOfType<TimeBtnController>().rolePlayTimeCurrent;

        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            GameManager.instance.myColor[i] = playerColor[i].color;
        }

        PhotonNetwork.LoadLevel("Stage");
    }
    public void RoomCreateExit()
    {
        lobbyMenuUI.SetActive(true);
        roomCreateUI.SetActive(false);

        FindObjectOfType<PunManager>().LeaveRoom();
    }
}
