using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Role
{
    public RoleData roleData;
    public int roleCnt = 0;
    public Color roleColor;

    public bool isImposter;
    public bool isNeutral;

    public Role (RoleData roleData, int roleCnt, Color roleColor, bool isImposter, bool isNeutral)
    {
        this.roleData = roleData;
        this.roleCnt = roleCnt;
        this.roleColor = roleColor;
        this.isImposter = isImposter;
        this.isNeutral = isNeutral;
    }
}
public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("GameManager already load.");
            Destroy(gameObject);
        }
    }
    #endregion

    //역할 정보 담는 변수
    public List<Role> roles = new List<Role>();

    //투표, 역할 시간 담는 변수
    public int voteTime;
    public int rolePlayTime;

    //플레이어 색 담는 변수, 플레이어 생성 되면 제작
    public Color myColor;

    //역할 수 담는 변수
    public int citizenNum;
    public int imposterNum;
    public int neutralNum;

    public int totalRoleNum;


}
