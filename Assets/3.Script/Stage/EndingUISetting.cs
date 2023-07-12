using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EndingUISetting : MonoBehaviour
{
    [Header("[Ending UI List]")]
    [SerializeField] Image titleImg;
    [SerializeField] GameObject[] crown;

    [SerializeField] GameObject[] playerList;

    [SerializeField] Image[] playerListImg;
    [SerializeField] Image[] playerImg;
    [SerializeField] Text[] playerNameTxt;
    [SerializeField] Image[] playerRoleImg;
    [SerializeField] Text[] PlayerRoleTxt;

    [Header("[Color List]")]
    [SerializeField] Color citizen;
    [SerializeField] Color imposter;
    [SerializeField] Color neutral;

    [SerializeField] EndingUI_playerCnt endingUI;

    private void OnEnable()
    {
        Set();
    }
    private void Set()
    {
        for (int i = 0; i < GameManager.instance.totalRoleNum; i++)
        {
            playerList[i].SetActive(true);

            for (int j = 0; j < GameManager.instance.totalRoleNum; j++)
            {
                Color color = GameManager.instance.roles[i].roleColor;
                color.a = 0.5f;
                playerListImg[i].color = color;

                playerRoleImg[i].sprite = GameManager.instance.roles[i].roleData.roleImg;
                PlayerRoleTxt[i].text = GameManager.instance.roles[i].roleData.roleName;

                if (GameManager.instance.roles[i] == GameManager.instance.shuffleRoles[j])
                {
                    playerImg[i].color = GameManager.instance.myColor[j];
                    playerNameTxt[i].text = PhotonNetwork.PlayerList[j].NickName;
                }
            }
        }
    }
    public void WinnerSet(int num)
    {
        if (num == 1)
        {
            titleImg.color = citizen;
            for(int i = 0; i < endingUI.citizenCnt; i++)
            {
                crown[i].SetActive(true);
            }
        }
        else if (num == 2)
        {
            titleImg.color = imposter;
            for (int i = endingUI.citizenCnt; i < endingUI.citizenCnt + endingUI.imposterCnt; i++)
            {
                crown[i].SetActive(true);
            }
        }
        else if (num == 3)
        {
            for (int i = endingUI.citizenCnt + endingUI.imposterCnt; i < endingUI.citizenCnt + endingUI.imposterCnt + endingUI.neutralCnt; i++)
            {
                crown[i].SetActive(true);
            }
            titleImg.color = neutral;
        }
    }
}
