using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PunManager : MonoBehaviourPunCallbacks
{
    //서버 접속 : Master Server -> Lobby -> Room
    private readonly string gameversion = "1";

    [Header("Server Setting")]
    public ServerSettings setting = null;

    [Header("ETC UI")]
    public InputField createRoomName;
    public InputField enterRoomName;
    public Text UserCountText;

    [Header("[Player Name UI Array]")]
    [SerializeField] GameObject playerNameUIPrebs;

    private LobbyBtnController lobbyBtnController;

    //플레이어 이름 default 값
    private Vector3 defaultPos = new Vector3(0, 500, 0);
    private Vector3 movePos = new Vector3(0, 100, 0);

    private PhotonView PV;

    private void Awake()
    {
        lobbyBtnController = FindObjectOfType<LobbyBtnController>();
    }
    private void Start()
    {
        Connect();
    }
    private void OnApplicationQuit()
    {
        Disconnect();
    }

    #region 서버 관련 콜백 함수들
    public void Connect()
    {
        PhotonNetwork.GameVersion = gameversion;

        //Master Server 에 연결
        PhotonNetwork.ConnectToMaster(setting.AppSettings.Server, setting.AppSettings.Port, "");

        Debug.Log("Connect to Master Server...");
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect(); //포톤 서버와 연결 끊기
    }

    //콜백함수
    public void CreateRoom()
    {
        if (createRoomName.Equals(string.Empty))
        {
            Debug.Log("입력해 주십시오.");
            return;
        }

        PhotonNetwork.LocalPlayer.NickName = lobbyBtnController.playerName;

        //방 참가를 시도하고 실패하면 생성해서 방에 참가해야함
        PhotonNetwork.CreateRoom(createRoomName.text, new RoomOptions { MaxPlayers = 10 }, null);
    }
    public void EnterRoom()
    {
        if (enterRoomName.Equals(string.Empty))
        {
            Debug.Log("입력해 주십시오.");
            return;
        }

        PhotonNetwork.LocalPlayer.NickName = lobbyBtnController.playerName;

        PhotonNetwork.JoinRoom(enterRoomName.text);
    }
    public void cancelMatching()
    {
        Debug.Log("방 나감");

        PhotonNetwork.LeaveRoom();
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connect Complete!");
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Connect to Room..");

        lobbyBtnController.RoomCreateOrEnter();

        PhotonNetwork.Instantiate(playerNameUIPrebs.name, Vector3.zero, Quaternion.identity);

        //for(int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        //{
        //    PhotonNetwork.PlayerList[i].SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "num", i } });
        //}

        //FindObjectOfType<ColorController>().DefaultColor();

        Update_Player();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //어떤 newPlayer 가 들어왔을 때 콜백되는 콜백함수
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log($"{newPlayer.NickName} enter room..");

        Update_Player();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //어떤 otherPlayer 가 방에서 나갔을 때 콜백되는 콜백함수
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log($"{otherPlayer.NickName} leave room..");

        Update_Player();
    }
    public void Update_Player()
    {
        UserCountText.text = $"{PhotonNetwork.CurrentRoom.PlayerCount}";
    }

    #endregion
}
