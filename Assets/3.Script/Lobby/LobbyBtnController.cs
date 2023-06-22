using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyBtnController : MonoBehaviour
{
    [Header("[LobbyMenu UI]")]
    [SerializeField] GameObject lobbyMenuUI;

    [Header("[RoomCreate UI]")]
    [SerializeField] GameObject roomCreateUI;

    [Header("[RoomEnter UI]")]
    [SerializeField] GameObject roomEnterUI;

    public void RoomCreate()
    {
        lobbyMenuUI.SetActive(false);
        roomCreateUI.SetActive(true);
    }
    public void RoomEnter()
    {

    }
    public void GameExit()
    {
        Application.Quit();
    }

    public void GameStart()
    {
        int citizenCnt = 0;
        int imposterCnt = 0;
        int neutralCnt = 0;
        
        //roleInfo 담기
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Role").Length; i++)
        {
            Role role = new Role(GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().roleData, GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().num, 
                GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().roleColor, GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().isImposter, GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().isNeutral);
            GameManager.instance.roles.Add(role);

            if(role.isImposter)
            {
                imposterCnt += role.roleCnt;
            }
            else if (role.isNeutral)
            {
                neutralCnt += role.roleCnt;
            }
            else
            {
                citizenCnt += role.roleCnt;
            }
        }

        GameManager.instance.citizenNum = citizenCnt;
        GameManager.instance.imposterNum = imposterCnt;
        GameManager.instance.neutralNum = neutralCnt;

        GameManager.instance.totalRoleNum = citizenCnt + imposterCnt + neutralCnt;

        //Time 담기
        GameManager.instance.voteTime = FindObjectOfType<TimeBtnController>().voteTimeCurrent;
        GameManager.instance.rolePlayTime = FindObjectOfType<TimeBtnController>().rolePlayTimeCurrent;

        SceneManager.LoadScene(1);
    }
    public void RoomCreateExit()
    {
        lobbyMenuUI.SetActive(true);
        roomCreateUI.SetActive(false);
    }
}
