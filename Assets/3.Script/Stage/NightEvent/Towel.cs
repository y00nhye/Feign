using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towel : MonoBehaviour
{
    Animator towelAni;
    Vector3 startPos;

    private void Awake()
    {
        TryGetComponent(out towelAni);
    }
    private void Start()
    {
        startPos = transform.position;
    }
    private void OnEnable()
    {
        towelAni.SetBool("Towel", true);

        Invoke("AniEnd", 3f);
    }
    private void AniEnd()
    {
        transform.position = startPos;
        towelAni.SetBool("Towel", false);
        gameObject.SetActive(false);
    }
}
