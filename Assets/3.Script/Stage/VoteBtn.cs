using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class VoteBtn : MonoBehaviour
{
    [Header("[Vote Num (set)]")]
    public int[] voteNum = new int[8];
    public int highNumIndex;

    [Header("[Vote Txt]")]
    [SerializeField] Text[] texts;

    [Header("[Vote Btns]")]
    [SerializeField] Button[] btns;

    [Header("[Choice Obj]")]
    [SerializeField] GameObject[] choice;

    [Header("[Status obj]")]
    [SerializeField] GameObject[] king;
    [SerializeField] GameObject[] blood;
    [SerializeField] GameObject[] check;

    private int rightVote; //≈ı«•±«

    private PhotonView PV;

    private int totalVote = 0;

    private bool isKingVote = false;
    private bool isCheck = false;
    private bool isKing = false;
    private bool isDieUIOn = false;

    [SerializeField] GameObject dieUI;

    private TimeManager timeManager;

    private void Awake()
    {
        TryGetComponent(out PV);
        timeManager = FindObjectOfType<TimeManager>();
    }
    private void Start()
    {
        rightVote = 1;
        totalVote = 0;
    }
    private void Update()
    {
        if (totalVote > 0)
        {
            isCheck = false;
        }

        if ((totalVote == GameManager.instance.currentPlayer || timeManager.currentTime == 0) && !isCheck)
        {
            isCheck = true;
            VoteCheck();
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
    public void ChoiceOn(GameObject choice)
    {
        for (int i = 0; i < btns.Length; i++)
        {
            btns[i].interactable = false;
        }

        choice.SetActive(true);
    }
    public void ChoiceOff(GameObject choice)
    {
        for (int i = 0; i < btns.Length; i++)
        {
            btns[i].interactable = true;
        }

        choice.SetActive(false);
    }
    public void Skip_pv()
    {
        if (rightVote > 0)
        {
            PV.RPC("Skip", RpcTarget.AllBuffered, rightVote);
            choice[0].SetActive(false);
            check[0].SetActive(true);

            rightVote = 0;
        }
    }
    [PunRPC]
    private void Skip(int voteRight)
    {
        Vote(texts[0], 0, voteRight);
    }
    public void Num1_pv()
    {
        if (rightVote > 0)
        {
            PV.RPC("Num1", RpcTarget.AllBuffered, rightVote);
            choice[1].SetActive(false);
            check[1].SetActive(true);

            rightVote = 0;
        }
    }
    [PunRPC]
    private void Num1(int voteRight)
    {
        Vote(texts[1], 1, voteRight);
    }
    public void Num2_pv()
    {
        if (rightVote > 0)
        {
            PV.RPC("Num2", RpcTarget.AllBuffered, rightVote);
            choice[2].SetActive(false);
            check[2].SetActive(true);

            rightVote = 0;
        }
    }
    [PunRPC]
    private void Num2(int voteRight)
    {
        Vote(texts[2], 2, voteRight);
    }
    public void Num3_pv()
    {
        if (rightVote > 0)
        {
            PV.RPC("Num3", RpcTarget.AllBuffered, rightVote);
            choice[3].SetActive(false);
            check[3].SetActive(true);

            rightVote = 0;
        }
    }
    [PunRPC]
    private void Num3(int voteRight)
    {
        Vote(texts[3], 3, voteRight);
    }
    public void Num4_pv()
    {
        if (rightVote > 0)
        {
            PV.RPC("Num4", RpcTarget.AllBuffered, rightVote);
            choice[4].SetActive(false);
            check[4].SetActive(true);

            rightVote = 0;
        }
    }
    [PunRPC]
    private void Num4(int voteRight)
    {
        Vote(texts[4], 4, voteRight);
    }
    public void Num5_pv()
    {
        if (rightVote > 0)
        {
            PV.RPC("Num5", RpcTarget.AllBuffered, rightVote);
            choice[5].SetActive(false);
            check[5].SetActive(true);

            rightVote = 0;
        }
    }
    [PunRPC]
    private void Num5(int voteRight)
    {
        Vote(texts[5], 5, voteRight);
    }
    public void Num6_pv()
    {
        if (rightVote > 0)
        {
            PV.RPC("Num6", RpcTarget.AllBuffered, rightVote);
            choice[6].SetActive(false);
            check[6].SetActive(true);

            rightVote = 0;
        }
    }
    [PunRPC]
    private void Num6(int voteRight)
    {
        Vote(texts[6], 6, voteRight);
    }
    public void Num7_pv()
    {
        if (rightVote > 0)
        {
            PV.RPC("Num7", RpcTarget.AllBuffered, rightVote);

            choice[7].SetActive(false);
            check[7].SetActive(true);

            rightVote = 0;
        }
    }
    [PunRPC]
    private void Num7(int voteRight)
    {
        Vote(texts[7], 7, voteRight);
    }
    public void Num8_pv()
    {
        if (rightVote > 0)
        {
            PV.RPC("Num8", RpcTarget.AllBuffered, rightVote);

            choice[8].SetActive(false);
            check[8].SetActive(true);

            rightVote = 0;
        }
    }
    [PunRPC]
    private void Num8(int voteRight)
    {
        Vote(texts[8], 8, voteRight);
    }

    private void Vote(Text txt, int num, int voteRight)
    {
        voteNum[num] += voteRight;
        txt.text = "" + voteNum[num];

        totalVote++;
    }
    private void VoteCheck()
    {
        if (timeManager.currentTime > 3)
        {
            timeManager.currentTime = 3;
        }
    }
    public void VoteReset()
    {
        highNumIndex = VoteCount();

        for (int i = 0; i < voteNum.Length; i++)
        {
            voteNum[i] = 0;
            texts[i].text = "0";
            btns[i].interactable = true;
            check[i].SetActive(false);
        }

        totalVote = 0;

        if (!isKingVote)
        {
            isKingVote = true;

            if (highNumIndex != 0)
            {
                king[highNumIndex - 1].SetActive(true);

                if ((int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"] == (highNumIndex - 1))
                {
                    isKing = true;
                }
            }
        }
        else
        {
            if (highNumIndex != 0)
            {
                GameManager.instance.playerPrefs[highNumIndex - 1].GetComponent<PlayerController>().isOut = true;
                if (GameManager.instance.playerPrefs[highNumIndex - 1].GetComponent<PlayerController>().myRole.isImposter)
                {
                    GameManager.instance.imposterNum--;
                }
                else if (GameManager.instance.playerPrefs[highNumIndex - 1].GetComponent<PlayerController>().myRole.isNeutral)
                {
                    GameManager.instance.neutralNum--;
                }
                else
                {
                    GameManager.instance.citizenNum--;
                }
                FindObjectOfType<FocusCamController>().dieCheck.Add(highNumIndex - 1);

                if ((int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"] == (highNumIndex - 1))
                {
                    timeManager.isFinish = true;
                }
            }
        }

        if (isKing)
        {
            rightVote = 2;
        }
        else
        {
            rightVote = 1;
        }

        highNumIndex = 0;
    }
    private int VoteCount()
    {
        int highNum = 0;
        int highNumIndex = 0;

        for (int i = 0; i < voteNum.Length; i++)
        {
            if (voteNum[i] > highNum)
            {
                highNum = voteNum[i];
                highNumIndex = i;
            }
            else if (voteNum[i] == highNum)
            {
                int rand = Random.Range(0, 2);

                highNum = (rand == 0) ? voteNum[i] : highNum;
                highNumIndex = (rand == 0) ? i : highNumIndex;
            }
        }

        return highNumIndex;
    }

}
