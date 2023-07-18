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
    [SerializeField] Color textColor;

    [Header("[Player Name UI]")]
    [SerializeField] Image[] playerNameUIPreb;

    [Header("[Chat Txt]")]
    [SerializeField] Text[] chatTxt;
    [SerializeField] InputField chatInput;

    private LobbyBtnController lobbyBtnController;
    private ColorController colorController;

    [SerializeField] PhotonView PV;

    [SerializeField] Text[] playerNameTxt;

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
            if (chatInput.text == "")
            {
                chatInput.ActivateInputField();
                return;
            }

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
        colorController.ColorReset();

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
        //room.CustomRoomProperties = new Hashtable() { { "0", -1 }, { "1", -1 }, { "2", -1 }, { "3", -1 }, { "4", -1 }, { "5", -1 }, { "6", -1 }, { "7", -1 } };
        

        PhotonNetwork.CreateRoom(createRoomName.text, room, null);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);

        createRoomName.text = "";
        createRoomName.GetComponentInChildren<Text>().text = "Already exists!";
        createRoomName.GetComponentInChildren<Text>().color = textColor;
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);

        enterRoomName.text = "";
        enterRoomName.GetComponentInChildren<Text>().text = "Does not exist!";
        enterRoomName.GetComponentInChildren<Text>().color = textColor;
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

        //PhotonNetwork.Instantiate(playerNameUIPreb.name, Vector3.zero, Quaternion.identity);

        MyNumSet(PhotonNetwork.CurrentRoom.PlayerCount - 1);
        Update_Player();

        NameSet();
        SendInfo();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //어떤 newPlayer 가 들어왔을 때 콜백되는 콜백함수
        base.OnPlayerEnteredRoom(newPlayer);
        Chatting("<color=orange>" + newPlayer.NickName + "님이 참가하셨습니다.</color>");

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            lobbyBtnController.MasterSet();
        }

        Update_Player();

        NameSet();

        SendInfo();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //어떤 otherPlayer 가 방에서 나갔을 때 콜백되는 콜백함수
        base.OnPlayerLeftRoom(otherPlayer);
        Chatting("<color=orange>" + otherPlayer.NickName + "님이 퇴장하셨습니다.</color>");

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            lobbyBtnController.MasterSet();
        }



        Update_Player();

        NameSet();

        colorController.SetColor();
    }
    public void MyNumSet(int myNum)
    {
        Debug.Log(myNum);

        PhotonNetwork.LocalPlayer.CustomProperties = new Hashtable()
        {
            {"myNum", myNum }
        };
    }
    public void NameSet()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            playerNameUIPreb[i].gameObject.SetActive(true);
            playerNameTxt[i].text = PhotonNetwork.PlayerList[i].NickName;

            lobbyBtnController.playerColor[i] = playerNameUIPreb[i];
        }
    }
    public void SendInfo()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount - 1; i++)
            {
                colorController.SetColor_pv(colorController.useColor[i], i);
            }

            colorController.DefaultColor_pv();
        }
    }
    public void Update_Player()
    {
        UserCountText.text = $"{PhotonNetwork.CurrentRoom.PlayerCount}";
    }
    public void Send()
    {
        string chat = PhotonNetwork.NickName + " : " + chatInput.text;
        PV.RPC("Chatting", RpcTarget.All, chat);
        chatInput.text = "";
    }

    [PunRPC]
    void Chatting(string chat)
    {
        bool isInput = false;
        for (int i = 0; i < chatTxt.Length; i++)
        {
            if (chatTxt[i].text == "")
            {
                isInput = true;

                for (int j = i; j > 0; j--)
                {
                    chatTxt[j].text = chatTxt[j - 1].text;
                }

                chatTxt[0].text = chat;
                break;
            }
        }
        if (!isInput)
        {
            for (int i = chatTxt.Length - 1; i > 0; i--)
            {
                chatTxt[i].text = chatTxt[i - 1].text;
            }
            chatTxt[0].text = chat;
        }
    }
    #endregion
}
