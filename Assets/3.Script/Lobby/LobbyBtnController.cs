using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyBtnController : MonoBehaviour
{
    [Header("[UI]")]
    [SerializeField] GameObject lobbyMenuUI;
    [SerializeField] GameObject roomCreateUI;
    [SerializeField] GameObject roomEnterUI;

    [Header("[Player Info]")]
    [SerializeField] Image playerColor;
    [SerializeField] Text playerName;

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

        List<Role> citizenRole = new List<Role>();
        List<Role> imposterRole = new List<Role>();
        List<Role> neutralRole = new List<Role>();

        //roleInfo 담기
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Role").Length; i++)
        {
            Role role = new Role(GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().roleData, GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().num, 
                GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().roleColor, GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().isImposter, GameObject.FindGameObjectsWithTag("Role")[i].GetComponent<RoleInfo>().isNeutral);

            if(role.isImposter)
            {
                imposterRole.Add(role);
                
                imposterCnt += role.roleCnt;
            }
            else if (role.isNeutral)
            {
                neutralRole.Add(role);
                
                neutralCnt += role.roleCnt;
            }
            else
            {
                citizenRole.Add(role);
                
                citizenCnt += role.roleCnt;
            }
        }

        GameManager.instance.roles = citizenRole;

        for (int i = 0; i < imposterRole.Count; i++)
        {
            GameManager.instance.roles.Add(imposterRole[i]);
        }
        for (int i = 0; i < neutralRole.Count; i++)
        {
            GameManager.instance.roles.Add(neutralRole[i]);
        }

        GameManager.instance.citizenNum = citizenCnt;
        GameManager.instance.imposterNum = imposterCnt;
        GameManager.instance.neutralNum = neutralCnt;

        GameManager.instance.totalRoleNum = citizenCnt + imposterCnt + neutralCnt;

        GameManager.instance.RoleShuffle();

        //Time 담기
        GameManager.instance.voteTime = FindObjectOfType<TimeBtnController>().voteTimeCurrent;
        GameManager.instance.rolePlayTime = FindObjectOfType<TimeBtnController>().rolePlayTimeCurrent;

        GameManager.instance.myColor = playerColor.color;
        GameManager.instance.myName = playerName.text;

        SceneManager.LoadScene(1);
    }
    public void RoomCreateExit()
    {
        //방 세팅 리셋 만들기
        
        lobbyMenuUI.SetActive(true);
        roomCreateUI.SetActive(false);
    }
}
