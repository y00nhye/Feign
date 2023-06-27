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
    public InputField roomName; //커스텀 프로퍼티
    public Text UserCountText; //User count
    public Button roomCreateBtn;
    public Button roomExitBtn;

    [Header("Player Prefabs")]
    public GameObject playerNamePrebs;

    //플레이어 이름 default 값
    private Vector3 defaultPos = new Vector3(0, 500, 0);
    private Vector3 movePos = new Vector3(0, 100, 0);

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

        roomCreateBtn.onClick.AddListener(JoinRandomRoomOrCreateRoom);
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect(); //포톤 서버와 연결 끊기
    }

    //콜백함수
    public void JoinRandomRoomOrCreateRoom()
    {
        if (roomName.Equals(string.Empty))
        {
            Debug.Log("입력해 주십시오.");
            return;
        }

        string nickname = FindObjectOfType<LobbyBtnController>().playerName;
        Debug.Log($"{FindObjectOfType<LobbyBtnController>().playerName} 랜덤 매칭 시작");
        PhotonNetwork.LocalPlayer.NickName = nickname;

        string roomname = roomName.text;

        RoomOptions option = new RoomOptions();

        int i_maxplayer = 8;

        option.MaxPlayers = i_maxplayer;

        //실질적으로 사용할 커스텀 프로퍼티 객체 생성
        option.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable()
        {
            {"RoomName", roomname }
        };
        //CustomRoomPropertiesForLobby : 로비에서 커스텀 프로퍼티 등록, 게임에서 필터링이 가능함.
        option.CustomRoomPropertiesForLobby = new string[] { "RoomName" };

        //방 참가를 시도하고 실패하면 생성해서 방에 참가해야함
        PhotonNetwork.JoinRandomOrCreateRoom(expectedCustomRoomProperties: new ExitGames.Client.Photon.Hashtable() { { "RoomName", option.CustomRoomProperties["RoomName"] } }, expectedMaxPlayers: (byte)option.MaxPlayers, roomOptions: option);
    }
    public void cancelMatching()
    {
        Debug.Log("매칭 취소");
        Debug.Log("방을 떠남");
        PhotonNetwork.LeaveRoom();
        roomExitBtn.onClick.RemoveAllListeners();
        roomExitBtn.onClick.AddListener(JoinRandomRoomOrCreateRoom);
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connect Complete!");

        PhotonNetwork.JoinLobby(); //대기실 입장
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        Debug.Log("Connect to Lobby..");

        PhotonNetwork.JoinRandomRoom(); //방 입장
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Connect to Room..");

        FindObjectOfType<LobbyBtnController>().RoomCreateOrEnter();
        GameObject name = Instantiate(playerNamePrebs);

        name.transform.SetParent(GameObject.Find("PlayerListBackground").transform);
        name.transform.localPosition = defaultPos - movePos;

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
