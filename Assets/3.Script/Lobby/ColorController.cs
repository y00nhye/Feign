using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ColorController : MonoBehaviour
{
    [Header("[Player Color (set)]")]
    public Image[] playerColor;

    [Header("[0Mint 1Blue 2Purple 3Yellow 4Gray 5Pink 6Orange 7Green]")]
    [SerializeField] Color[] colors;

    [Header("[Use Color Gameobject]")]
    [SerializeField] GameObject[] useSprite;

    //사용한 컬러 담는 변수
    public List<int> useColor = new List<int>();

    private PhotonView PV;

    private void Awake()
    {
        PV = GameObject.Find("LobbyManager").GetPhotonView();
    }
    private void Start()
    {
        playerColor = new Image[8];
    }

    public void DefaultColor_pv(int playerNum)
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            if (playerColor[i] != null && playerColor[i].gameObject.GetComponent<PhotonView>().IsMine)
            {
                PV.RPC("DefaultColor", RpcTarget.AllBuffered, playerNum);
            }
        }
    }

    [PunRPC]
    private void DefaultColor(int playerNum)
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            if (!useSprite[i].activeSelf)
            {
                playerColor[playerNum].color = colors[i];
                GameManager.instance.myColorNum[playerNum] = i;

                useColor.Add(i);
                useSprite[i].SetActive(true);

                break;
            }
        }
    }

    public void ColorSet_pv(int colorNum)
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            if (playerColor[i].gameObject.GetComponent<PhotonView>().IsMine)
            {
                PV.RPC("ColorSet", RpcTarget.AllBuffered, colorNum, i);
            }
        }
    }

    [PunRPC]
    private void ColorSet(int colorNum, int playerNum)
    {
        for (int i = 0; i < useColor.Count; i++)
        {
            if (useColor[i] == colorNum)
            {
                return;
            }
        }

        useSprite[GameManager.instance.myColorNum[playerNum]].SetActive(false);
        useColor.Remove(GameManager.instance.myColorNum[playerNum]);

        playerColor[playerNum].color = colors[colorNum];

        GameManager.instance.myColorNum[playerNum] = colorNum;

        useColor.Add(colorNum);
        useSprite[colorNum].SetActive(true);
    }
}
