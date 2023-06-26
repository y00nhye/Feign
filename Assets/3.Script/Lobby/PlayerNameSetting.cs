using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameSetting : MonoBehaviour
{
    [Header("[Player Name Txt]")]
    [SerializeField] private Text playerName;

    private void Awake()
    {
        playerName.text = FindObjectOfType<LobbyBtnController>().playerName;

        //생성과 동시에 다른 클래스 컬러 변수 채워주기
        FindObjectOfType<LobbyBtnController>().playerColor = gameObject.GetComponent<Image>();
        FindObjectOfType<ColorController>().playerColor = gameObject.GetComponent<Image>();

        FindObjectOfType<ColorController>().DefaultColor();
    }
}
