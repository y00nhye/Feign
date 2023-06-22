using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBtnController : MonoBehaviour
{
    [Header("[Time Text UI]")]
    [SerializeField] Text voteTimeTxt;
    [SerializeField] Text rolePlayTimeTxt;

    [Header("[Current Time (Set)]")]
    public int voteTimeCurrent;
    public int rolePlayTimeCurrent;

    //타이머 기본값
    private int voteTimeDefault = 180;
    private int rolePlayTimeDefault = 30;

    //타이머 변동 수치값 변수
    private int voteTimeValue = 60;
    private int rolePlayTimeValue = 10;

    private void Start()
    {
        voteTimeCurrent = voteTimeDefault;
        rolePlayTimeCurrent = rolePlayTimeDefault;
    }
    public void VoteTimeUp()
    {
        voteTimeCurrent += voteTimeValue;

        if(voteTimeCurrent > 300)
        {
            voteTimeCurrent = 300;
        }

        voteTimeTxt.text = "" + voteTimeCurrent;
    }
    public void VoteTimeDown()
    {
        voteTimeCurrent -= voteTimeValue;

        if (voteTimeCurrent < 60)
        {
            voteTimeCurrent = 60;
        }

        voteTimeTxt.text = "" + voteTimeCurrent;
    }
    public void RolePlayTimeUp()
    {
        rolePlayTimeCurrent += rolePlayTimeValue;

        if (rolePlayTimeCurrent > 60)
        {
            rolePlayTimeCurrent = 60;
        }

        rolePlayTimeTxt.text = "" + rolePlayTimeCurrent;
    }
    public void RolePlayTimeDown()
    {
        rolePlayTimeCurrent -= rolePlayTimeValue;

        if (rolePlayTimeCurrent < 10)
        {
            rolePlayTimeCurrent = 10;
        }

        rolePlayTimeTxt.text = "" + rolePlayTimeCurrent;
    }
}
