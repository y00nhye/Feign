using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //플레이어 스크립트 가져오기
    private PlayerInput playerInput;
    private Animator playerAni;
    private Rigidbody playerRigid;

    //이동 및 회전 속도 수치값 변수
    private float moveSpeed = 3f;
    private float rotateSpeed = 120f;

    [Header("[0Mint 1Blue 2Purple 3Yellow 4Gray 5Pink 6Orange 7Green]")]
    [SerializeField] Material[] materials;

    [Header("[Player Info]")]
    [SerializeField] Image myColor;
    [SerializeField] Text myNickname;
    [SerializeField] SkinnedMeshRenderer slimeColor;

    private void Awake()
    {
        TryGetComponent(out playerInput);
        TryGetComponent(out playerAni);
        TryGetComponent(out playerRigid);
    }
    private void Start()
    {
        myColor.color = GameManager.instance.myColor;
        myNickname.text = GameManager.instance.myName;
        slimeColor.material = materials[GameManager.instance.myColorNum];
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
