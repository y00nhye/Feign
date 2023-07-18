using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ColorController : MonoBehaviour
{
    [Header("[Player Color]")]
    public Image[] playerColor;

    [Header("[0Mint 1Blue 2Purple 3Yellow 4Gray 5Pink 6Orange 7Green]")]
    [SerializeField] Color[] colors;

    [Header("[Use Color Gameobject]")]
    [SerializeField] GameObject[] useSprite;

    //사용한 컬러 담는 변수
    public int[] useColor;

    public int leftNum;

    private PhotonView PV;

    private void Awake()
    {
        PV = GameObject.Find("LobbyManager").GetPhotonView();
    }
    public void SetColor_pv(int num, int index)
    {
        PV.RPC("SetColor", RpcTarget.OthersBuffered, num, index);
    }
    public void DefaultColor_pv()
    {
        PV.RPC("DefaultColor", RpcTarget.AllBuffered);
    }
    [PunRPC]
    private void SetColor(int num, int index)
    {
        playerColor[index].color = colors[num];
        useColor[index] = num;
        useSprite[num].SetActive(true);
    }
    [PunRPC]
    private void DefaultColor()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            if (!useSprite[i].activeSelf)
            {
                playerColor[PhotonNetwork.CurrentRoom.PlayerCount - 1].color = colors[i];
                //GameManager.instance.myColorNum[PhotonNetwork.CurrentRoom.PlayerCount - 1] = i;

                useColor[PhotonNetwork.CurrentRoom.PlayerCount - 1] = i;
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

        useSprite[useColor[playerNum]].SetActive(false);
        useColor[playerNum] = -1;

        playerColor[playerNum].color = colors[colorNum];

        //GameManager.instance.myColorNum[playerNum] = colorNum;

        useColor[playerNum] = colorNum;
        useSprite[colorNum].SetActive(true);
    }

    [PunRPC]
    private void ColorRemove(int playerNum)
    {
        playerColor[PhotonNetwork.CurrentRoom.PlayerCount - 1].gameObject.SetActive(false);

        useSprite[useColor[playerNum]].SetActive(false);
        useColor[playerNum] = -1;

        for (int i = playerNum; i < useColor.Length - 1; i++)
        {
            useColor[i] = useColor[i + 1];
        }

        if ((int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"] > playerNum)
        {
            FindObjectOfType<PunManager>().MyNumSet((int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"] - 1);
        }
    }
    public void ColorReset()
    {
        for (int i = 0; i < useColor.Length; i++)
        {
            useColor[i] = -1;
            useSprite[i].SetActive(false);
        }
    }
    public void SetColor()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            playerColor[i].color = colors[useColor[i]];
        }
    }
}
