using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Chatting : MonoBehaviourPun
{
    [SerializeField] PhotonView PV;

    [Header("[Chat Txt]")]
    [SerializeField] Text[] chatTxt;
    [SerializeField] InputField chatInput;

    bool isDayUI = false;
    bool isNightUI = false;

    int timeUICheck_day = 5;
    int timeUICheck_night = 5;

    [SerializeField] GameObject dayChatUI;
    [SerializeField] GameObject nightChatUI;

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
    public void Send()
    {
        string chat = "( " + PhotonNetwork.NickName + " ) " + chatInput.text;
        PV.RPC("Chat", RpcTarget.All, chat);
        chatInput.text = "";
    }

    [PunRPC]
    void Chat(string chat)
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

                    if (j - 1 == timeUICheck_day)
                    {
                        dayChatUI.transform.position = chatTxt[j].transform.position;
                        timeUICheck_day++;
                    }
                    if (j - 1 == timeUICheck_night)
                    {
                        nightChatUI.transform.position = chatTxt[j].transform.position;
                        timeUICheck_night++;
                    }
                }

                chatTxt[0].text = chat;
                break;
            }
        }
        if (!isInput)
        {
            if(timeUICheck_day == 4)
            {
                dayChatUI.SetActive(false);
                timeUICheck_day++;
            }
            if (timeUICheck_night == 4)
            {
                nightChatUI.SetActive(false);
                timeUICheck_night++;
            }

            for (int i = chatTxt.Length - 1; i > 0; i--)
            {
                chatTxt[i].text = chatTxt[i - 1].text;

                if (i - 1 == timeUICheck_day)
                {
                    dayChatUI.transform.position = chatTxt[i].transform.position;
                    timeUICheck_day++;
                }
                if (i - 1 == timeUICheck_night)
                {
                    nightChatUI.transform.position = chatTxt[i].transform.position;
                    timeUICheck_night++;
                }
            }
            chatTxt[0].text = chat;
        }
    }

    public void TimeLog(GameObject timeLog)
    {
        Chat(" ");

        if (timeLog == dayChatUI)
        {
            dayChatUI.SetActive(true);
            dayChatUI.transform.position = chatTxt[0].transform.position;

            timeUICheck_day = 0;
        }
        else if (timeLog == nightChatUI)
        {
            nightChatUI.SetActive(true);
            nightChatUI.transform.position = chatTxt[0].transform.position;

            timeUICheck_night = 0;
        }
    }

    public void SystemSend(string systemChat)
    {
        string chat = "<color=yellow>(System)" + systemChat + "</color>";
        Chat(chat);
    }
}
