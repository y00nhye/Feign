using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class PunManager : MonoBehaviourPunCallbacks
{
    //서버 접속 : Master Server -> Lobby -> Room
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

    #region 서버 관련 콜백 함수들
    public void Connect()
    {
        PhotonNetwork.GameVersion = gameversion;

        //Master Server 에 연결
        PhotonNetwork.ConnectToMaster(setting.AppSettings.Server, setting.AppSettings.Port, "");
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect(); //포톤 서버와 연결 끊기
    }

    public void LeaveRoom()
    {
        colorController.ColorRemove_pv();

        PhotonNetwork.LeaveRoom();
    }

    //콜백함수
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

        //방 참가를 시도하고 실패하면 생성해서 방에 참가해야함
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
        //어떤 newPlayer 가 들어왔을 때 콜백되는 콜백함수
        base.OnPlayerEnteredRoom(newPlayer);
        PV.RPC("Chatting", RpcTarget.All, "<color=yellow>" + newPlayer.NickName + "님이 참가하셨습니다.</color>");

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            lobbyBtnController.MasterSet();
        }

        Update_Player();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //어떤 otherPlayer 가 방에서 나갔을 때 콜백되는 콜백함수
        base.OnPlayerLeftRoom(otherPlayer);
        PV.RPC("Chatting", RpcTarget.All, "<color=yellow>" + otherPlayer.NickName + "님이 퇴장하셨습니다.</color>");

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
