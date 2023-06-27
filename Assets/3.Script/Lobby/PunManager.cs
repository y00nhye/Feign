using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PunManager : MonoBehaviourPunCallbacks
{
    //���� ���� : Master Server -> Lobby -> Room
    private readonly string gameversion = "1";

    [Header("Server Setting")]
    public ServerSettings setting = null;

    [Header("ETC UI")]
    public InputField roomName; //Ŀ���� ������Ƽ
    public Text UserCountText; //User count
    public Button roomCreateBtn;
    public Button roomExitBtn;

    [Header("Player Prefabs")]
    public GameObject playerNamePrebs;

    //�÷��̾� �̸� default ��
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

    #region ���� ���� �ݹ� �Լ���
    public void Connect()
    {
        PhotonNetwork.GameVersion = gameversion;

        //Master Server �� ����
        PhotonNetwork.ConnectToMaster(setting.AppSettings.Server, setting.AppSettings.Port, "");

        Debug.Log("Connect to Master Server...");

        roomCreateBtn.onClick.AddListener(JoinRandomRoomOrCreateRoom);
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect(); //���� ������ ���� ����
    }

    //�ݹ��Լ�
    public void JoinRandomRoomOrCreateRoom()
    {
        if (roomName.Equals(string.Empty))
        {
            Debug.Log("�Է��� �ֽʽÿ�.");
            return;
        }

        string nickname = FindObjectOfType<LobbyBtnController>().playerName;
        Debug.Log($"{FindObjectOfType<LobbyBtnController>().playerName} ���� ��Ī ����");
        PhotonNetwork.LocalPlayer.NickName = nickname;

        string roomname = roomName.text;

        RoomOptions option = new RoomOptions();

        int i_maxplayer = 8;

        option.MaxPlayers = i_maxplayer;

        //���������� ����� Ŀ���� ������Ƽ ��ü ����
        option.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable()
        {
            {"RoomName", roomname }
        };
        //CustomRoomPropertiesForLobby : �κ񿡼� Ŀ���� ������Ƽ ���, ���ӿ��� ���͸��� ������.
        option.CustomRoomPropertiesForLobby = new string[] { "RoomName" };

        //�� ������ �õ��ϰ� �����ϸ� �����ؼ� �濡 �����ؾ���
        PhotonNetwork.JoinRandomOrCreateRoom(expectedCustomRoomProperties: new ExitGames.Client.Photon.Hashtable() { { "RoomName", option.CustomRoomProperties["RoomName"] } }, expectedMaxPlayers: (byte)option.MaxPlayers, roomOptions: option);
    }
    public void cancelMatching()
    {
        Debug.Log("��Ī ���");
        Debug.Log("���� ����");
        PhotonNetwork.LeaveRoom();
        roomExitBtn.onClick.RemoveAllListeners();
        roomExitBtn.onClick.AddListener(JoinRandomRoomOrCreateRoom);
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connect Complete!");

        PhotonNetwork.JoinLobby(); //���� ����
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        Debug.Log("Connect to Lobby..");

        PhotonNetwork.JoinRandomRoom(); //�� ����
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
        //� newPlayer �� ������ �� �ݹ�Ǵ� �ݹ��Լ�
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log($"{newPlayer.NickName} enter room..");
        Update_Player();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //� otherPlayer �� �濡�� ������ �� �ݹ�Ǵ� �ݹ��Լ�
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
