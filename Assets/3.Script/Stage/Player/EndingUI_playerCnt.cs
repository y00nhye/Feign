using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingUI_playerCnt : MonoBehaviour
{
    public int citizenCnt;
    public int imposterCnt;
    public int neutralCnt;

    private void Start()
    {
        citizenCnt = GameManager.instance.citizenNum;
        imposterCnt = GameManager.instance.imposterNum;
        neutralCnt = GameManager.instance.neutralNum;
    }
}
