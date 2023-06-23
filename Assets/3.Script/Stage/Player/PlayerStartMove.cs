using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartMove : MonoBehaviour
{
    //플레이어 순서에 맞는 지연시간 정하기
    private float waitTime = 7 + GameManager.instance.myOrder;

    //이동 및 회전 속도
    private float rotateSpeed = 5f;
    private float moveSpeed = 0.005f;

    private Animator playerAni;

    [Header("[Vote Position]")]
    [SerializeField] Transform[] votePos;

    private void Awake()
    {
        TryGetComponent(out playerAni);
    }
    private void Start()
    {
        StartCoroutine(StartMove_co());
    }

    IEnumerator StartMove_co()
    {
        yield return new WaitForSeconds(waitTime);

        while (true)
        {
            Vector3 dir = votePos[GameManager.instance.myOrder].transform.position - transform.position;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);

            yield return null;

        }

        while (Vector3.Distance(transform.position, votePos[GameManager.instance.myOrder].position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, votePos[GameManager.instance.myOrder].position, moveSpeed);

            playerAni.SetFloat("Speed", 1);

            yield return null;
        }

        playerAni.SetFloat("Speed", 0);
        transform.position = votePos[GameManager.instance.myOrder].position;
    }
}
