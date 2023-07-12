using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerCanvas : MonoBehaviour
{
    private Animator canvasAni;

    private PhotonView PV;

    public PlayerController playerController;

    [Header("[Player Info]")]
    [SerializeField] Image myRoleColor;
    [SerializeField] Image myRoleImg;
    [SerializeField] Text myRoleTxt;

    private void Awake()
    {
        TryGetComponent(out canvasAni);
        playerController = transform.parent.GetComponent<PlayerController>();
        PV = transform.parent.gameObject.GetPhotonView();
    }
    private void Start()
    {
        if (PV.IsMine)
        {
            CanvasAnimation();
        }
    }
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    public void CanvasAnimation()
    {
        Invoke("AniOn", 1f);

        Invoke("AniShow", 2f);

        Invoke("AniOff", 5f);

        Invoke("AniReset", 7f);
    }

    private void AniOn()
    {
        canvasAni.SetBool("On", true);
    }
    private void AniShow()
    {
        canvasAni.SetBool("Show", true);

        if (!playerController.isClean)
        {
            myRoleColor.color = playerController.myRole.roleColor;
            myRoleImg.sprite = playerController.myRole.roleData.roleImg;
            myRoleTxt.text = playerController.myRole.roleData.roleName;
        }
    }
    private void AniOff()
    {
        canvasAni.SetBool("Off", true);
    }
    private void AniReset()
    {
        canvasAni.SetBool("On", false);
        canvasAni.SetBool("Show", false);
        canvasAni.SetBool("Off", false);
    }
}
