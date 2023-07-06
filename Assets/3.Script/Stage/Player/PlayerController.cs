using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    //�÷��̾� ��ũ��Ʈ ��������
    private PlayerInput playerInput;
    private Animator playerAni;
    private Rigidbody playerRigid;

    //�̵� �� ȸ�� �ӵ� ��ġ�� ����
    private float moveSpeed = 3f;
    private float rotateSpeed = 120f;

    [Header("[0Mint 1Blue 2Purple 3Yellow 4Gray 5Pink 6Orange 7Green]")]
    [SerializeField] Material[] materials;

    [Header("[Player Info]")]
    [SerializeField] Image myColor;
    [SerializeField] Text myNickname;
    [SerializeField] SkinnedMeshRenderer slimeColor;
    [SerializeField] Image myRoleColor;
    [SerializeField] Image myRoleImg;
    [SerializeField] Text myRoleTxt;

    [Header("[Player Role (set)]")]
    public Role myRole;

    public int myNum;

    private PhotonView PV;

    private void Awake()
    {
        TryGetComponent(out playerInput);
        TryGetComponent(out playerAni);
        TryGetComponent(out playerRigid);
        TryGetComponent(out PV);
    }
    private void Start()
    {
        myNum = (int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"];

        if (PV.IsMine)
        {
            PV.RPC("Set", RpcTarget.AllBuffered, myNum);

            myRole = GameManager.instance.shuffleRoles[myNum];
            myRoleColor.color = myRole.roleColor;
            myRoleImg.sprite = myRole.roleData.roleImg;
            myRoleTxt.text = myRole.roleData.roleName;
        }
    }
    [PunRPC]
    private void Set(int num)
    {
        myColor.color = GameManager.instance.myColor[num];
        myNickname.text = PV.Controller.NickName;
        slimeColor.material = materials[num];
    }
    private void Update()
    {
        Rotate();
        Move();

        if (playerInput.move > 0)
        {
            playerAni.SetFloat("Speed", Mathf.Abs(playerInput.move));
        }
    }

    private void Move()
    {
        Vector3 moveDistance = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
        playerRigid.MovePosition(playerRigid.position + moveDistance);

    }
    private void Rotate()
    {
        float turn = playerInput.rotate * rotateSpeed * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0, turn, 0);
    }
}
