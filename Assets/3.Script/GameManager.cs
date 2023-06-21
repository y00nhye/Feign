using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    #region Singleton
    public static GameInfo instance = null;

    private void Awake()
    {
        if(instance == null)
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

    public RoleInfo[] roles;

    public Color myColor;

    public int voteTime;
    public int rolePlayTime;

    //여기에 정보 담아서 들고갑시다~
}
