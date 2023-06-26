using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("[Time Check (set)]")]
    public bool isDay = false;
    public bool isVote = false;
    public bool isNight = false;
    public bool isLoading = false;
    public bool isTimeChange = false; //시간 바뀔 때 true 변경

    private int currentTime = 0;

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
                dayTimeUI.SetActive(false);
                voteTimeUI.SetActive(true);
                nightTimeUI.SetActive(false);

                currentTime = GameManager.instance.voteTime;
                time.text = "" + GameManager.instance.voteTime;

                StartCoroutine(Timer_co());

                isVote = false;
            }
            else if (isNight)
            {
                dayTimeUI.SetActive(false);
                voteTimeUI.SetActive(false);
                nightTimeUI.SetActive(true);

                currentTime = GameManager.instance.rolePlayTime;
                time.text = "" + GameManager.instance.rolePlayTime;

                StartCoroutine(Timer_co());

                isNight = false;
            }

            if (isLoading)
            {
                time.gameObject.SetActive(false);
                hourglass.SetActive(true);

                isLoading = false;
            }

            isTimeChange = false;
        }
    }

    IEnumerator Timer_co()
    {
        while (currentTime != 0)
        {
            yield return new WaitForSeconds(1f);

            currentTime -= 1;
            time.text = "" + currentTime;
        }
    }
}
