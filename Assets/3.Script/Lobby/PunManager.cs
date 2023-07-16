using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using ExitGames.Client.Photon;

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

    [Header("[Chat Txt]")]
    [SerializeField] Text[] chatTxt;
    [SerializeField] InputField chatInput;

    private LobbyBtnController lobbyBtnController;
    private ColorController colorController;

    [SerializeField] PhotonView PV;

    private void Awake()
    {
        lobbyBtnController = FindObjectOfType<LobbyBtnController>();
        colorController = FindObjectOfType<ColorController>();
    }
    private void Start()
    {
        Connect();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Send();

            chatInput.ActivateInputField();
        }
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

        RoomOptions room = new RoomOptions();

        room.MaxPlayers = 8;
        room.CustomRoomProperties = new Hashtable() { { "Load", false }};

        //�� ������ �õ��ϰ� �����ϸ� �����ؼ� �濡 �����ؾ���
        PhotonNetwork.CreateRoom(createRoomName.text, room, null);
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
        PV.RPC("Chatting", RpcTarget.All, "<color=yellow>" + newPlayer.NickName + "���� �����ϼ̽��ϴ�.</color>");

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
        PV.RPC("Chatting", RpcTarget.All, "<color=yellow>" + otherPlayer.NickName + "���� �����ϼ̽��ϴ�.</color>");

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

        PhotonNetwork.LocalPlayer.CustomProperties = new Hashtable()
        {
            {"myNum", myNum }
        };

    }
    public void Update_Player()
    {
        UserCountText.text = $"{PhotonNetwork.CurrentRoom.PlayerCount}";
    }
    public void Send()
    {
        string chat = PhotonNetwork.NickName + " : " + chatInput.text;
        PV.RPC("Chatting", RpcTarget.All, PhotonNetwork.NickName + " : " + chatInput.text);
        chatInput.text = "";
    }

    [PunRPC]
    void Chatting(string chat)
    {
        bool isInput = false;
        for(int i = 0; i < chatTxt.Length; i++)
        {
            if (chatTxt[i].text == "")
            {
                isInput = true;
                chatTxt[i].text = chat;
                break;
            }
        }
        if (!isInput)
        {
            for(int i = 1; i < chatTxt.Length; i++)
            {
                chatTxt[i - 1].text = chatTxt[i].text;
                chatTxt[chatTxt.Length - 1].text = chat;
            }
        }
    }
    #endregion
}
