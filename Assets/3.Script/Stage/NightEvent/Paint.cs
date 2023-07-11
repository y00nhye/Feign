using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
    Animator paintAni;

    private void Awake()
    {
        TryGetComponent(out paintAni);
    }
    private void OnEnable()
    {
        paintAni.SetBool("Paint", true);
        StartCoroutine(SetFalse());
    }
    IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(3f);

        paintAni.SetBool("Paint", false);
        gameObject.SetActive(false);
    }
}
