using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayer : MonoBehaviour
{
    [Header("[Jump Timing (write)]")]
    [SerializeField] int jumpTiming;
    [SerializeField] float jumpCycle;

    //�÷��̾� �ִϸ�����
    private Animator ani;
    //�÷��̾� ���� ���� üũ
    private bool isJump = false;

    private void Awake()
    {
        TryGetComponent(out ani);
    }
    private void Start()
    {
        JumpTime();
    }
    private void Update()
    {
        if (isJump)
        {
            StartCoroutine(JumpCycle_co());
        }
    }
    private void JumpTime()
    {
        Invoke("JumpAni", jumpTiming);
        isJump = true;
    }
    private void JumpAni()
    {
        ani.SetTrigger("Jump");
    }
    IEnumerator JumpCycle_co()
    {
        isJump = false;

        yield return new WaitForSeconds(jumpCycle);
        JumpAni();

        isJump = true;
    }
}
