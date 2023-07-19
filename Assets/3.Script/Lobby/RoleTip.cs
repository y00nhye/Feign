using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleTip : MonoBehaviour
{
    [SerializeField] GameObject[] Tips;

    public void MouseOverTip(int num)
    {
        Tips[num].SetActive(true);
    }
    public void MouseExitTip(int num)
    {
        Tips[num].SetActive(false);
    }
}
