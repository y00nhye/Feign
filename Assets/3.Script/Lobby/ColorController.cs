using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{
    [Header("[Player Color]")]
    [SerializeField] Image playerColor;

    [Header("[0Mint 1Blue 2Purple 3Yellow 4Gray 5Pink 6Orange 7Green]")]
    [SerializeField] Color[] colors;

    [Header("[Use Color Gameobject]")]
    [SerializeField] GameObject[] useSprite;

    //사용한 컬러 담는 변수
    private List<int> useColor = new List<int>();

    private void Start()
    {
        playerColor.color = colors[0];
        GameManager.instance.myColorNum = 0;

        useColor.Add(0);
        useSprite[0].SetActive(true);
    }

    public void ColorSet(int colorNum)
    {
        for (int i = 0; i < useColor.Count; i++)
        {
            if (useColor[i] != colorNum)
            {
                useSprite[GameManager.instance.myColorNum].SetActive(false);
                useColor.Remove(GameManager.instance.myColorNum);
                
                playerColor.color = colors[colorNum];

                GameManager.instance.myColorNum = colorNum;

                useColor.Add(colorNum);
                useSprite[colorNum].SetActive(true);

                break;
            }
        }
    }
}
