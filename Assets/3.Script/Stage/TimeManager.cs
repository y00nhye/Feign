using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class TimeManager : MonoBehaviour
{
    [Header("[Time Txt]")]
    [SerializeField] Text time;

    [Header("[Time UI]")]
    [SerializeField] GameObject dayTimeUI;
    [SerializeField] GameObject voteTimeUI;
    [SerializeField] GameObject nightTimeUI;

    [Header("[Time Status]")]
    [SerializeField] GameObject hourglass;

    [Header("[Event UI]")]
    [SerializeField] GameObject voteUI;
    [SerializeField] GameObject rolePlayingUI;
    [SerializeField] GameObject kingUI;

    [Header("[Chinemachine camera]")]
    [SerializeField] CinemachineVirtualCamera voteCam;
    [SerializeField] CinemachineVirtualCamera nightCam;

    [Header("[PostProcessing]")]
    [SerializeField] GameObject nightPostProcessing;

    [Header("[Time Check (set)]")]
    public bool isDay = false;
    public bool isVote = false;
    public bool isNight = false;
    public bool isLoading = false;
    public bool isTimeChange = false; //시간 바뀔 때 true 변경

    public bool nightMove = false;
    public bool dayMove = false;
    public bool rolePlayingSet = false;
    public bool voteSet = false;
    public bool isFinish = false;

    private bool isKingVote = true;

    [Header("[Current Time (set)]")]
    public int currentTime = 0;

    [Header("[TimeChat UI]")]
    public GameObject dayChatUI;
    public GameObject nightChatUI;

    [SerializeField] VoteBtn voteBtn;

    private EventUIBtn eventUI;
    private Chatting chatting;

    private void Awake()
    {
        eventUI = FindObjectOfType<EventUIBtn>();
        chatting = FindObjectOfType<Chatting>();
    }
    private void Start()
    {
        GameManager.instance.timeManager = GetComponent<TimeManager>();
    }
    private void Update()
    {
        if (isTimeChange)
        {
            time.gameObject.SetActive(true);
            hourglass.SetActive(false);

            if (isDay)
            {
                voteCam.enabled = true;
                nightPostProcessing.SetActive(false);

                dayTimeUI.SetActive(true);
                voteTimeUI.SetActive(false);
                nightTimeUI.SetActive(false);

                isLoading = true;
            }
            else if (isVote)
            {
                chatting.TimeLog(dayChatUI);
                
                if (isKingVote)
                {
                    chatting.SystemSend("시장 투표를 시작합니다. 시장은 다음 투표에서 표를 두배로 가집니다.");
                    isKingVote = false;
                }
                else
                {
                    chatting.SystemSend("추방 투표를 시작합니다. 임포스터로 의심되는 사람을 투표하세요.");
                }

                eventUI.VoteBtn();

                dayTimeUI.SetActive(false);
                voteTimeUI.SetActive(true);
                nightTimeUI.SetActive(false);

                voteUI.SetActive(true);
                rolePlayingUI.SetActive(false);

                currentTime = GameManager.instance.voteTime;
                time.text = "" + GameManager.instance.voteTime;

                StartCoroutine(Timer_co());
            }
            else if (isNight)
            {
                chatting.TimeLog(nightChatUI);

                chatting.SystemSend("밤이 되었습니다. 각자의 능력을 사용하세요.");

                if (!isFinish)
                {
                    voteCam.enabled = false;
                }
                nightPostProcessing.SetActive(true);

                eventUI.NightBtn();

                dayTimeUI.SetActive(false);
                voteTimeUI.SetActive(false);
                nightTimeUI.SetActive(true);

                voteUI.SetActive(false);
                rolePlayingUI.SetActive(true);

                currentTime = GameManager.instance.rolePlayTime;
                time.text = "" + GameManager.instance.rolePlayTime;

                StartCoroutine(Timer_co());
            }

            if (isLoading)
            {
                eventUI.LoadingBtn();

                time.gameObject.SetActive(false);
                hourglass.SetActive(true);

                isLoading = false;
            }

            isTimeChange = false;
        }
    }

    IEnumerator Timer_co()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);

            currentTime -= 1;
            time.text = "" + currentTime;
        }

        if (isVote)
        {
            voteBtn.VoteReset();

            isVote = false;
            voteSet = true;
        }
        else if (isNight)
        {
            FindObjectOfType<EventUIBtn>().RolePlayingBtn();

            rolePlayingSet = true;
        }
    }

    public void NightOn()
    {
        isTimeChange = true;
        isNight = true;
    }
    public void DayOn()
    {
        isTimeChange = true;
        isDay = true;
    }
}
