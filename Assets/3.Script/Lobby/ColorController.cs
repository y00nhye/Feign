using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ColorController : MonoBehaviour
{
    [Header("[Player Color (set)]")]
    public Image[] playerColor;

    [Header("[0Mint 1Blue 2Purple 3Yellow 4Gray 5Pink 6Orange 7Green]")]
    [SerializeField] Color[] colors;

    [Header("[Use Color Gameobject]")]
    [SerializeField] GameObject[] useSprite;

    //사용한 컬러 담는 변수
    public int[] useColor;

    private PhotonView PV;

    private void Awake()
    {
        PV = GameObject.Find("LobbyManager").GetPhotonView();
    }
    private void Start()
    {
        playerColor = new Image[8];
    }
    public void SetColor_pv(int num)
    {
        PV.RPC("SetColor", RpcTarget.OthersBuffered, num);
    }
    [PunRPC]
    private void SetColor(int num)
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount - 1; i++)
        {
            playerColor[i].color = colors[num];
        }
    }
    public void DefaultColor(int playerNum)
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            if (!useSprite[i].activeSelf)
            {
                playerColor[playerNum].color = colors[i];
                GameManager.instance.myColorNum[playerNum] = i;

                useColor[playerNum] = i;
                useSprite[i].SetActive(true);

                break;
            }
        }
    }
    public void ColorSet_pv(int colorNum)
    {
        PV.RPC("ColorSet", RpcTarget.AllBuffered, colorNum, (int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]);
    }

    public void ColorRemove_pv()
    {
        PV.RPC("ColorRemove", RpcTarget.AllBuffered, (int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]);
    }

    [PunRPC]
    private void ColorSet(int colorNum, int playerNum)
    {
        for (int i = 0; i < useColor.Length; i++)
        {
            if (useColor[i] == colorNum)
            {
                return;
            }
        }

        useSprite[GameManager.instance.myColorNum[playerNum]].SetActive(false);
        useColor[playerNum] = -1;

        playerColor[playerNum].color = colors[colorNum];

        GameManager.instance.myColorNum[playerNum] = colorNum;

        useColor[playerNum] = colorNum;
        useSprite[colorNum].SetActive(true);
    }

    [PunRPC]
    private void ColorRemove(int playerNum)
    {
        PhotonNetwork.CurrentRoom.CustomProperties[playerNum.ToString()] = -1;
        PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);

        useSprite[GameManager.instance.myColorNum[playerNum]].SetActive(false);
        useColor[playerNum] = -1;
    }
}
