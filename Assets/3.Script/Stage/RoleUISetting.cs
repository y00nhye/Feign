using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleUISetting : MonoBehaviour
{
    [Header("[Role Img UI]")]
    [SerializeField] Image[] roleImg;
    [SerializeField] RectTransform bgTransform;

    private void Start()
    {
        RoleSet();
        BGSet();
    }

    private void RoleSet()
    {
        int j = 0;

        for (int i = 0; i < GameManager.instance.roles.Count; i++)
        {
            for (int k = 0; k < GameManager.instance.roles[i].roleCnt; k++)
            {
                roleImg[j].GetComponentsInChildren<Image>()[1].sprite = GameManager.instance.roles[i].roleData.roleImg;
                roleImg[j].color = GameManager.instance.roles[i].roleColor;

                j++;
            }
        }

        for (int i = roleImg.Length - 1; i > GameManager.instance.totalRoleNum - 1; i--)
        {
            roleImg[i].gameObject.SetActive(false);
        }
    }
    private void BGSet()
    {
        bgTransform.offsetMax = new Vector2(-(560 - ((GameManager.instance.roles.Count - 1) * 80)), bgTransform.sizeDelta.y);
    }
}
