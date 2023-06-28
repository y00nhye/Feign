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
    public Text UserCountText;

    [Header("[Player Name UI Array]")]
    [SerializeField] GameObject playerNameUIPrebs;

    private LobbyBtnController lobbyBtnController;

    //�÷��̾� �̸� default ��
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

    #region ���� ���� �ݹ� �Լ���
    public void Connect()
    {
        PhotonNetwork.GameVersion = gameversion;

        //Master Server �� ����
        PhotonNetwork.ConnectToMaster(setting.AppSettings.Server, setting.AppSettings.Port, "");

        Debug.Log("Connect to Master Server...");
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect(); //���� ������ ���� ����
    }

    //�ݹ��Լ�
    public void CreateRoom()
    {
        if (createRoomName.Equals(string.Empty))
        {
            Debug.Log("�Է��� �ֽʽÿ�.");
            return;
        }

        PhotonNetwork.LocalPlayer.NickName = lobbyBtnController.playerName;

        //�� ������ �õ��ϰ� �����ϸ� �����ؼ� �濡 �����ؾ���
        PhotonNetwork.CreateRoom(createRoomName.text, new RoomOptions { MaxPlayers = 10 }, null);
    }
    public void EnterRoom()
    {
        if (enterRoomName.Equals(string.Empty))
        {
            Debug.Log("�Է��� �ֽʽÿ�.");
            return;
        }

        PhotonNetwork.LocalPlayer.NickName = lobbyBtnController.playerName;

        PhotonNetwork.JoinRoom(enterRoomName.text);
    }
    public void cancelMatching()
    {
        Debug.Log("�� ����");

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
