using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    private Animator canvasAni;

    [Header("[Active PlayUI (set)]")]
    [SerializeField] GameObject playUI;

    private void Awake()
    {
        TryGetComponent(out canvasAni);
        playUI = GameObject.Find("PlayUI");
    }
    private void Start()
    {
        CanvasAnimation();
    }
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    private void CanvasAnimation()
    {
        Invoke("AniOn", 1f);

        Invoke("AniShow", 2f);

        Invoke("AniOff", 5f);

        Invoke("PlayUIOn", 6f);
    }

    private void AniOn()
    {
        canvasAni.SetBool("On", true);
    }
    private void AniShow()
    {
        canvasAni.SetBool("Show", true);
    }
    private void AniOff()
    {
        canvasAni.SetBool("Off", true);
    }
    private void PlayUIOn()
    {
        playUI.SetActive(true);
    }
}
