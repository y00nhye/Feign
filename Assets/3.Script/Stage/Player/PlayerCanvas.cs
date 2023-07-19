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
    public Image myRoleColor;
    public Image myRoleImg;
    public Text myRoleTxt;

    [Header("[ETC]")]
    [SerializeField] Color imposter;

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
        AudioManager.instance.OpenSound();
        canvasAni.SetBool("Show", true);

        if (playerController.myRole.roleData.roleName == "정신병자" && !playerController.isDie_ && !playerController.isOut)
        {
            myRoleColor.color = playerController.rolePsy.roleColor;
            myRoleImg.sprite = playerController.rolePsy.roleData.roleImg;
            myRoleTxt.text = playerController.rolePsy.roleData.roleName;
        }
        else if (!playerController.isClean && !playerController.isPaint)
        {
            myRoleColor.color = playerController.myRole.roleColor;
            myRoleImg.sprite = playerController.myRole.roleData.roleImg;
            myRoleTxt.text = playerController.myRole.roleData.roleName;
        }
        else if (playerController.isPaint)
        {
            myRoleColor.color = imposter;
            myRoleImg.sprite = playerController.myRole.roleData.roleImg;
            myRoleTxt.text = playerController.myRole.roleData.roleName;
        }
    }
    private void AniOff()
    {
        canvasAni.SetBool("Off", true);

        AudioManager.instance.OpenSound();
    }
    private void AniReset()
    {
        canvasAni.SetBool("On", false);
        canvasAni.SetBool("Show", false);
        canvasAni.SetBool("Off", false);
    }
}
