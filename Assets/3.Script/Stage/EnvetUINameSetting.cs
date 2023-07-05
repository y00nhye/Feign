using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EnvetUINameSetting : MonoBehaviour
{
    [Header("[Player Name UI]")]
    [SerializeField] GameObject[] votePlayerNameUI;
    [SerializeField] GameObject[] rolePlayingPlayerUI;

    [Header("[Player Name Txt]")]
    [SerializeField] Text[] votePlayerNameTxt;
    [SerializeField] Text[] rolePlayingPlayerTxt;

    [Header("[Player Name Img]")]
    [SerializeField] Image[] votePlayerNameImg;
    [SerializeField] Image[] rolePlayingPlayerImg;

    [Header("[Event UI]")]
    [SerializeField] RectTransform background;

    private void Start()
    {
        PlayerUISet();

        BGSet();
    }

    private void PlayerUISet()
    {
        for (int i = 0; i < GameManager.instance.roles.Count; i++)
        {
            votePlayerNameUI[i].SetActive(true);
            rolePlayingPlayerUI[i].SetActive(true);
        }

        for (int i = 0; i < GameManager.instance.roles.Count; i++)
        {
            votePlayerNameTxt[i].text = PhotonNetwork.PlayerList[i].NickName;
            rolePlayingPlayerTxt[i].text = PhotonNetwork.PlayerList[i].NickName;

            votePlayerNameImg[i].color = GameManager.instance.myColor[i];
            rolePlayingPlayerImg[i].color = GameManager.instance.myColor[i];
        }
    }
    private void BGSet()
    {
        background.offsetMax = new Vector2(background.sizeDelta.x - 180, -(115 - ((GameManager.instance.roles.Count) * 75)));
    }
}
