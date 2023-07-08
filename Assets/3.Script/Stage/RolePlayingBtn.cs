using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RolePlayingBtn : MonoBehaviour
{
    [Header("[Role Playing Button]")]
    [SerializeField] Button[] playerBtn;
    [SerializeField] Button[] role_1Btn;
    [SerializeField] Button[] role_2Btn;

    [Header("[Role Playing Obj]")]
    [SerializeField] GameObject[] check;
    [SerializeField] GameObject role1;
    [SerializeField] GameObject role2;

    [Header("[Role Plaing Img]")]
    [SerializeField] Sprite DieImg;
    [SerializeField] Image role_01Img;
    [SerializeField] Image role_11Img;
    [SerializeField] Image role_12Img;

    private void Start()
    {
        Set();
    }

    private void Set()
    {
        for (int i = 0; i < role_1Btn.Length; i++)
        {
            role_1Btn[i].image.sprite = GameManager.instance.shuffleRoles[(int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]].roleData.roleImg;
        }

        if (!GameManager.instance.shuffleRoles[(int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]].isImposter)
        {
            role1.SetActive(true);
            role_01Img.sprite = GameManager.instance.shuffleRoles[(int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]].roleData.roleImg;

            for (int i = 0; i < role_2Btn.Length; i++)
            {
                role_2Btn[i].gameObject.SetActive(false);
            }
        }
        else
        {
            role2.SetActive(true);
            role_11Img.sprite = GameManager.instance.shuffleRoles[(int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]].roleData.roleImg;
            role_12Img.sprite = DieImg;

            for (int i = 0; i < role_2Btn.Length; i++)
            {
                role_2Btn[i].image.sprite = DieImg;
            }
        }

    }

    public void CheckOn(GameObject che)
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            check[i].SetActive(false);
        }

        che.SetActive(true);
    }
}
