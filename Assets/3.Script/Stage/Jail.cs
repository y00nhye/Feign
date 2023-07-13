using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jail : MonoBehaviour
{
    Animator jailAni;

    private void Awake()
    {
        TryGetComponent(out jailAni);
    }
    public void AniStart()
    {
        jailAni.SetBool("Jail", true);
    }
}
