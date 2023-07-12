using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FocusCamController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera[] focusCam;

    private TimeManager timeManager;

    public List<int> dieCheck = new List<int>();

    private void Awake()
    {
        timeManager = FindObjectOfType<TimeManager>();
    }
    private void Update()
    {
        if (timeManager.isDay)
        {
            timeManager.isDay = false;

            if (dieCheck.Count > 0)
            {
                StartCoroutine(Focus(dieCheck));
            }
            else
            {
                timeManager.isTimeChange = true;
                timeManager.isVote = true;
            }
        }
    }
    IEnumerator Focus(List<int> playerNum)
    {
        yield return new WaitForSeconds(3f);

        for (int i = 0; i < playerNum.Count; i++)
        {
            GameManager.instance.currentPlayer--;

            focusCam[playerNum[i]].enabled = true;
            GameManager.instance.playerPrefs[playerNum[i]].GetComponentInChildren<PlayerCanvas>().CanvasAnimation();

            yield return new WaitForSeconds(8f);

            focusCam[playerNum[i]].enabled = false;

            yield return new WaitForSeconds(1f);
        }

        timeManager.isTimeChange = true;
        timeManager.isVote = true;
    }
}
