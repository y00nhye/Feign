using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FocusCamController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera[] focusCam;

    private TimeManager timeManager;

    public List<int> dieCheck = new List<int>();

    private bool isDay = false;

    private void Awake()
    {
        timeManager = FindObjectOfType<TimeManager>();
    }
    private void Update()
    {
        if (timeManager.isDay)
        {
            timeManager.isDay = false;
            isDay = true;

            if (dieCheck.Count > 0)
            {
                StartCoroutine(Focus(dieCheck, isDay));
            }
            else
            {
                timeManager.isTimeChange = true;
                timeManager.isVote = true;
            }
        }
        else if (timeManager.voteSet)
        {
            timeManager.voteSet = false;
            isDay = false;

            if (dieCheck.Count > 0)
            {
                StartCoroutine(Focus(dieCheck, isDay));
            }
            else
            {
                timeManager.nightMove = true;

                Invoke("NightOn", 1f);
            }
        }
    }
    IEnumerator Focus(List<int> playerNum, bool isDay)
    {
        yield return new WaitForSeconds(3f);

        for (int i = 0; i < playerNum.Count; i++)
        {
            GameManager.instance.currentPlayer--;

            focusCam[playerNum[i]].enabled = true;

            GameManager.instance.playerPrefs[playerNum[i]].GetComponentInChildren<PlayerCanvas>().CanvasAnimation();
            yield return new WaitForSeconds(7f);

            if (!isDay)
            {
                GameManager.instance.playerPrefs[playerNum[i]].GetComponentInChildren<Jail>().AniStart();
                yield return new WaitForSeconds(1f);
            }

            focusCam[playerNum[i]].enabled = false;
        }

        dieCheck = new List<int>();

        if (!GameManager.instance.GameOverCheck())
        {
            if (isDay)
            {
                timeManager.isTimeChange = true;
                timeManager.isVote = true;
            }
            else
            {
                yield return new WaitForSeconds(1f);

                timeManager.nightMove = true;

                yield return new WaitForSeconds(1f);

                timeManager.NightOn();
            }
        }
    }
    private void NightOn()
    {
        timeManager.NightOn();
    }
}
