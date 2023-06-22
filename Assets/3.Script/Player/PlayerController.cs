using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //플레이어 스크립트 가져오기
    private PlayerInput playerInput;
    private Animator playerAni;
    private Rigidbody playerRigid;

    //이동 및 회전 속도 수치값 변수
    private float moveSpeed = 3f;
    private float rotateSpeed = 120f;

    private void Start()
    {
        TryGetComponent(out playerInput);
        TryGetComponent(out playerAni);
        TryGetComponent(out playerRigid);
    }
    private void Update()
    {
        Rotate();
        Move();

        playerAni.SetFloat("Speed", playerInput.move);
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
