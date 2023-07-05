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
    public InputField createRoomName;
    public InputField enterRoomName;
    public Text RoomName;
    public Text UserCountText;

    [Header("[Player Name UI]")]
    [SerializeField] GameObject playerNameUIPreb;

    private LobbyBtnController lobbyBtnController;
    private ColorController colorController;

    private void Awake()
    {
        lobbyBtnController = FindObjectOfType<LobbyBtnController>();
        colorController = FindObjectOfType<ColorController>();
    }
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
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect(); //���� ������ ���� ����
    }

    public void LeaveRoom()
    {
        colorController.ColorRemove_pv();

        PhotonNetwork.LeaveRoom();
    }

    //�ݹ��Լ�
    public void CreateRoom()
    {
        if (createRoomName.Equals(string.Empty))
        {
            return;
        }

        PhotonNetwork.LocalPlayer.NickName = lobbyBtnController.playerName;
        RoomName.text = createRoomName.text;

        //�� ������ �õ��ϰ� �����ϸ� �����ؼ� �濡 �����ؾ���
        PhotonNetwork.CreateRoom(createRoomName.text, new RoomOptions { MaxPlayers = 8 }, null);
    }
    public void EnterRoom()
    {
        if (enterRoomName.Equals(string.Empty))
        {
            return;
        }

        PhotonNetwork.LocalPlayer.NickName = lobbyBtnController.playerName;
        RoomName.text = enterRoomName.text;

        PhotonNetwork.JoinRoom(enterRoomName.text);
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        lobbyBtnController.RoomCreateOrEnter();

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            lobbyBtnController.MasterSet();
        }

        PhotonNetwork.Instantiate(playerNameUIPreb.name, Vector3.zero, Quaternion.identity);

        MyNumSet();
        Update_Player();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //� newPlayer �� ������ �� �ݹ�Ǵ� �ݹ��Լ�
        base.OnPlayerEnteredRoom(newPlayer);

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            lobbyBtnController.MasterSet();
        }

        Update_Player();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //� otherPlayer �� �濡�� ������ �� �ݹ�Ǵ� �ݹ��Լ�
        base.OnPlayerLeftRoom(otherPlayer);

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            lobbyBtnController.MasterSet();
        }

        MyNumSet();
        Update_Player();
    }
    private void MyNumSet()
    {
        int myNum = PhotonNetwork.LocalPlayer.ActorNumber - 1;

        PhotonNetwork.LocalPlayer.CustomProperties = new ExitGames.Client.Photon.Hashtable()
        {
            {"myNum", myNum }
        };

    }
    public void Update_Player()
    {
        UserCountText.text = $"{PhotonNetwork.CurrentRoom.PlayerCount}";
    }

    #endregion
}
