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
    public Button[] btns;

    [Header("[Choice Obj]")]
    [SerializeField] GameObject[] Choice;

    [Header("[King obj]")]
    [SerializeField] GameObject[] king;

    private int rightVote; //≈ı«•±«

    private PhotonView PV;

    private int totalVote = 0;

    private bool isKingVote = false;
    private bool isCheck = false;
    private bool isKing = false;

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
            PV.RPC("Skip", RpcTarget.AllBuffered);

            rightVote--;
        }
    }
    [PunRPC]
    private void Skip()
    {
        Vote(texts[0], 0, Choice[0]);
    }
    public void Num1_pv()
    {
        if (rightVote > 0)
        {
            PV.RPC("Num1", RpcTarget.AllBuffered);

            rightVote--;
        }
    }
    [PunRPC]
    private void Num1()
    {
        Vote(texts[1], 1, Choice[1]);
    }
    public void Num2_pv()
    {
        if (rightVote > 0)
        {
            PV.RPC("Num2", RpcTarget.AllBuffered);

            rightVote--;
        }
    }
    [PunRPC]
    private void Num2()
    {
        Vote(texts[2], 2, Choice[2]);
    }
    public void Num3_pv()
    {
        if (rightVote > 0)
        {
            PV.RPC("Num3", RpcTarget.AllBuffered);

            rightVote--;
        }
    }
    [PunRPC]
    private void Num3()
    {
        Vote(texts[3], 3, Choice[3]);
    }
    public void Num4_pv()
    {
        if (rightVote > 0)
        {
            PV.RPC("Num4", RpcTarget.AllBuffered);

            rightVote--;
        }
    }
    [PunRPC]
    private void Num4()
    {
        Vote(texts[4], 4, Choice[4]);
    }
    public void Num5_pv()
    {
        if (rightVote > 0)
        {
            PV.RPC("Num5", RpcTarget.AllBuffered);

            rightVote--;
        }
    }
    [PunRPC]
    private void Num5()
    {
        Vote(texts[5], 5, Choice[5]);
    }
    public void Num6_pv()
    {
        if (rightVote > 0)
        {
            PV.RPC("Num6", RpcTarget.AllBuffered);

            rightVote--;
        }
    }
    [PunRPC]
    private void Num6()
    {
        Vote(texts[6], 6, Choice[6]);
    }
    public void Num7_pv()
    {
        if (rightVote > 0)
        {
            PV.RPC("Num7", RpcTarget.AllBuffered);

            rightVote--;
        }
    }
    [PunRPC]
    private void Num7()
    {
        Vote(texts[7], 7, Choice[7]);
    }
    public void Num8_pv()
    {
        if (rightVote > 0)
        {
            PV.RPC("Num8", RpcTarget.AllBuffered);

            rightVote--;
        }
    }
    [PunRPC]
    private void Num8()
    {
        Vote(texts[8], 8, Choice[8]);
    }

    private void Vote(Text txt, int num, GameObject choice)
    {
        choice.SetActive(false);

        voteNum[num]++;
        txt.text = "" + voteNum[num];

        totalVote++;
    }
    private void VoteCheck()
    {
        if (timeManager.currentTime > 1)
        {
            timeManager.currentTime = 3;
        }

        VoteReset();
    }
    private void VoteReset()
    {
        highNumIndex = VoteCount();

        for (int i = 0; i < voteNum.Length; i++)
        {
            voteNum[i] = 0;
            texts[i].text = "0";
            btns[i].interactable = true;
        }

        totalVote = 0;

        if (isKing)
        {
            rightVote = 2;
        }
        else
        {
            rightVote = 1;
        }

        if (!isKingVote)
        {
            isKingVote = true;

            if (highNumIndex == 0)
            {
                return;
            }

            king[highNumIndex - 1].SetActive(true);

            if ((int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"] == (highNumIndex - 1))
            {
                isKing = true;
            }
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
