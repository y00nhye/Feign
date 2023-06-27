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

        Debug.Log($"{FindObjectOfType<LobbyBtnController>().playerName} ���� ��Ī ����");

        PhotonNetwork.LocalPlayer.NickName = FindObjectOfType<LobbyBtnController>().playerName;

        //�� ������ �õ��ϰ� �����ϸ� �����ؼ� �濡 �����ؾ���
        PhotonNetwork.CreateRoom(createRoomName.text, new RoomOptions { MaxPlayers = 10 }, null);
    }
    public void EnterRoom()
    {
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
        FindObjectOfType<LobbyBtnController>().RoomCreateOrEnter();
        GameObject name = Instantiate(playerNamePrebs);

        name.transform.SetParent(GameObject.Find("PlayerListBackground").transform);
        name.transform.localPosition = defaultPos - movePos * PhotonNetwork.CurrentRoom.PlayerCount;

        UserCountText.text = $"{PhotonNetwork.CurrentRoom.PlayerCount}";
    }

    #endregion
}
