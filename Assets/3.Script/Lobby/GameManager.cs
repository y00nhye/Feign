using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[System.Serializable]
public class Role
{
    public RoleData roleData;
    public int roleCnt = 0;
    public int roleSerialNum;
    public Color roleColor;

    public bool isImposter;
    public bool isNeutral;

    public Role (RoleData roleData, int roleCnt, Color roleColor, bool isImposter, bool isNeutral, int roleSerialNum)
    {
        this.roleData = roleData;
        this.roleCnt = roleCnt;
        this.roleColor = roleColor;
        this.isImposter = isImposter;
        this.isNeutral = isNeutral;
        this.roleSerialNum = roleSerialNum;
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

    //���� ���� ��� ����
    public List<Role> roles = new List<Role>();
    public List<Role> shuffleRoles = new List<Role>();

    //��ǥ, ���� �ð� ��� ����
    public int voteTime;
    public int rolePlayTime;

    //�÷��̾� ���� ��� ����, �÷��̾� ���� �Ǹ� ����
    public Color[] myColor;
    public int[] myColorNum;

    //���� �� ��� ����
    public int citizenNum;
    public int imposterNum;
    public int neutralNum;

    public int totalRoleNum;

    private void Start()
    {
        myColor = new Color[8];
        myColorNum = new int[8];
    }

    public void RoleShuffle()
    {
        for(int i = 0; i < roles.Count; i++)
        {   
            shuffleRoles.Add(roles[i]);
        }

        int ran1;
        int ran2;

        Role temp;

        for(int i = 0; i < 10; i++)
        {
            ran1 = Random.Range(0, roles.Count);
            ran2 = Random.Range(0, roles.Count);

            temp = shuffleRoles[ran1];
            shuffleRoles[ran1] = shuffleRoles[ran2];
            shuffleRoles[ran2] = temp;
        }
    }

    //������ �س���, �÷��̾� ������� ���õ� role Ŭ���� ������ ��, �÷��̾��ʿ��� �̹��� ����, ���� �ɷ� �������ֱ�
}
