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

        //������ ���ÿ� �ٸ� Ŭ���� �÷� ���� ä���ֱ�
        FindObjectOfType<LobbyBtnController>().playerColor = gameObject.GetComponent<Image>();
        FindObjectOfType<ColorController>().playerColor = gameObject.GetComponent<Image>();

        FindObjectOfType<ColorController>().DefaultColor();
    }
}
