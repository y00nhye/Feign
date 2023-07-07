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

    [Header("[Chinemachine camera]")]
    [SerializeField] CinemachineVirtualCamera voteCam; 
    [SerializeField] CinemachineVirtualCamera nightCam;

    [Header("[Time Check (set)]")]
    public bool isDay = false;
    public bool isVote = false;
    public bool isNight = false;
    public bool isLoading = false;
    public bool isTimeChange = false; //�ð� �ٲ� �� true ����
    public bool nightMove = false;
    public bool dayMove = false;

    [Header("[Current Time (set)]")]
    public int currentTime = 0;

    private EventUIBtn eventUI;

    private void Awake()
    {
        eventUI = FindObjectOfType<EventUIBtn>();
    }
    private void Update()
    {
        if (isTimeChange)
        {
            time.gameObject.SetActive(true);
            hourglass.SetActive(false);

            if (isDay)
            {
                dayTimeUI.SetActive(true);
                voteTimeUI.SetActive(false);
                nightTimeUI.SetActive(false);

                isDay = false;
            }
            else if (isVote)
            {
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
                voteCam.enabled = false;

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
            nightMove = true;

            isVote = false;

            Invoke("NightOn", 1f);
        }
        else if (isNight)
        {
            voteCam.enabled = true;

            dayMove = true;
            
            isNight = false;

            Invoke("DayOn", 1f);
        }
    }

    private void NightOn()
    {
        isTimeChange = true;
        isNight = true;
    }
    private void DayOn()
    {
        isTimeChange = true;
        isDay = true;
        isLoading = true;
    }
}
