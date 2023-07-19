using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] Button[] Btns;

    private void Start()
    {
        for(int i = 0; i < Btns.Length; i++)
        {
            Btns[i].onClick.AddListener(AudioManager.instance.BtnSound);
        }
    }
}
