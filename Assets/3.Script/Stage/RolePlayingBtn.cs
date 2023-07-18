using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;

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
    [SerializeField] GameObject[] blood;

    [Header("[Role Plaing Img]")]
    [SerializeField] Sprite DieImg;
    [SerializeField] Image role_01Img;
    [SerializeField] Image role_11Img;
    [SerializeField] Image role_12Img;

    public Button eventUIBtn;
    public GameObject eventUI;

    public string[] actions;
    public bool[] isBlock;

    private PhotonView PV;

    private int ActiveNum = -1;

    private bool isKill = false;
    public bool isRolePlaying = false;
    private bool isDieUIOn = false;

    public int rolePlayingEnd = 0;

    [SerializeField] GameObject dieUI;

    private TimeManager timeManager;

    private void Awake()
    {
        TryGetComponent(out PV);
        timeManager = FindObjectOfType<TimeManager>();
    }
    private void Start()
    {
        Set();
        actions = new string[8];
        isBlock = new bool[8];
        for (int i = 0; i < isBlock.Length; i++)
        {
            isBlock[i] = false;
        }
    }
    private void Update()
    {
        if (timeManager.rolePlayingSet)
        {
            if (!isBlock[(int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]])
            {
                PV.RPC("Action", RpcTarget.AllBuffered, ActiveNum, (int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"], isKill);

                if (ActiveNum >= 0)
                {
                    if (int.Parse(GameManager.instance.shuffleRoles[(int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]].roleData.roleOrder) == 7)
                    {
                        GameManager.instance.playerPrefs[ActiveNum].GetComponentInChildren<PlayerCanvas>().myRoleImg.sprite = GameManager.instance.playerPrefs[ActiveNum].GetComponent<PlayerController>().myRole.roleData.roleImg;
                        GameManager.instance.playerPrefs[ActiveNum].GetComponentInChildren<PlayerCanvas>().myRoleColor.color = GameManager.instance.playerPrefs[ActiveNum].GetComponent<PlayerController>().myRole.roleColor;
                    }
                }
            }
            timeManager.rolePlayingSet = false;
            isRolePlaying = true;
        }

        if (rolePlayingEnd == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            RolePlayingEnd();
        }

        if (dieUI.activeSelf && !isDieUIOn)
        {
            isDieUIOn = true;

            PV.RPC("DieOn", RpcTarget.AllBuffered, (int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]);
        }
    }
    [PunRPC]
    private void DieOn(int playerNum)
    {
        blood[playerNum].SetActive(true);
    }
    private void Set()
    {
        for (int i = 0; i < role_1Btn.Length; i++)
        {
            if (GameManager.instance.shuffleRoles[(int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]].roleData.roleName != "정신병자")
            {
                role_1Btn[i].image.sprite = GameManager.instance.shuffleRoles[(int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]].roleData.roleImg;
            }
            else
            {
                role_1Btn[i].image.sprite = GameManager.instance.playerPrefs[(int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]].GetComponent<PlayerController>().rolePsy.roleData.roleImg;
            }

        }

        ButtonSet();

        if (!GameManager.instance.shuffleRoles[(int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]].isImposter)
        {
            role1.SetActive(true);
            if (GameManager.instance.shuffleRoles[(int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]].roleData.roleName != "정신병자")
            {
                role_01Img.sprite = GameManager.instance.shuffleRoles[(int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]].roleData.roleImg;
            }
            else
            {
                role_01Img.sprite = GameManager.instance.playerPrefs[(int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]].GetComponent<PlayerController>().rolePsy.roleData.roleImg;
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
    private void ButtonSet()
    {
        for (int i = 0; i < role_2Btn.Length; i++)
        {
            role_1Btn[i].gameObject.SetActive(true);
        }

        if (GameManager.instance.shuffleRoles[(int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]].isImposter)
        {
            for (int i = 0; i < role_2Btn.Length; i++)
            {
                role_2Btn[i].gameObject.SetActive(true);
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
    public void RolePlaying_1(int activeNum)
    {
        for (int i = 0; i < role_1Btn.Length; i++)
        {
            role_1Btn[i].gameObject.SetActive(false);
            role_2Btn[i].gameObject.SetActive(false);
        }

        role_1Btn[activeNum].gameObject.SetActive(true);
        role_1Btn[activeNum].interactable = false;

        for (int i = 0; i < role_1Btn.Length; i++)
        {
            playerBtn[i].interactable = false;
        }

        ActiveNum = activeNum;

        if (int.Parse(GameManager.instance.shuffleRoles[(int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]].roleData.roleOrder) == 0 ||
            int.Parse(GameManager.instance.shuffleRoles[(int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"]].roleData.roleOrder) == 1)
        {
            if ((int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"] != activeNum)
            {
                PV.RPC("ActionBlockCheck", RpcTarget.AllBuffered, activeNum);
            }
        }
    }
    public void RolePlaying_2(int activeNum)
    {
        for (int i = 0; i < role_1Btn.Length; i++)
        {
            role_1Btn[i].gameObject.SetActive(false);
            role_2Btn[i].gameObject.SetActive(false);
        }

        role_2Btn[activeNum].gameObject.SetActive(true);
        role_2Btn[activeNum].interactable = false;

        for (int i = 0; i < role_1Btn.Length; i++)
        {
            playerBtn[i].interactable = false;
        }

        ActiveNum = activeNum;

        isKill = true;

        if ((int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"] != activeNum)
        {
            PV.RPC("ActionBlockCheck", RpcTarget.AllBuffered, activeNum);
        }
    }
    [PunRPC]
    private void ActionBlockCheck(int num)
    {
        isBlock[num] = true;
    }
    [PunRPC]
    private void Action(int activeNum, int myNum, bool kill)
    {
        if (activeNum >= 0)
        {
            if (actions[activeNum] == null)
            {
                if (kill)
                {
                    actions[activeNum] += 2;
                }
                else
                {
                    if (int.Parse(GameManager.instance.shuffleRoles[myNum].roleData.roleOrder) != 7)
                    {
                        actions[activeNum] += GameManager.instance.shuffleRoles[myNum].roleData.roleOrder;
                    }
                }
            }
            else
            {
                if (kill)
                {
                    actions[activeNum] += "," + 2;
                }
                else
                {
                    if (int.Parse(GameManager.instance.shuffleRoles[myNum].roleData.roleOrder) != 7)
                    {
                        actions[activeNum] += "," + GameManager.instance.shuffleRoles[myNum].roleData.roleOrder;
                    }
                }
            }
        }
    }
    public int[] ActionConvertertoInt(int num)
    {
        if (actions[num] == null)
        {
            return null;
        }

        string[] serialNum_s = actions[num].Split(',');
        int[] serialNum_i = new int[serialNum_s.Length];

        for (int i = 0; i < serialNum_i.Length; i++)
        {
            serialNum_i[i] = int.Parse(serialNum_s[i]);
        }

        Array.Sort(serialNum_i);

        return serialNum_i;
    }
    public void RolePlayingReset()
    {
        for (int i = 0; i < role_1Btn.Length; i++)
        {
            role_1Btn[i].interactable = true;
            role_2Btn[i].interactable = true;
            playerBtn[i].interactable = true;
            check[i].SetActive(false);

            isBlock[i] = false;
            actions[i] = null;
        }

        ButtonSet();

        isKill = false;
        ActiveNum = -1;

        timeManager.isNight = false;
    }
    private void RolePlayingEnd()
    {
        rolePlayingEnd = 0;

        timeManager.dayMove = true;
        timeManager.DayOn();
    }
}
