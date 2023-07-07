using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerStartMove : MonoBehaviour
{
    //플레이어 순서에 맞는 지연시간 정하기
    private float waitTime;

    //이동 및 회전 속도
    private float rotateSpeed = 5f;
    private float moveSpeed = 0.005f;

    private Animator playerAni;

    [Header("[Vote Position]")]
    [SerializeField] Transform[] cornerPos;
    [SerializeField] Transform[] votePos;

    private PhotonView PV;
    private int myNum;

    private void Awake()
    {
        TryGetComponent(out playerAni);
        TryGetComponent(out PV);

        cornerPos = new Transform[4];
        votePos = new Transform[8];
    }
    private void Start()
    {
        for (int i = 0; i < cornerPos.Length; i++)
        {
            cornerPos[i] = GameObject.Find("Corner").GetComponentsInChildren<Transform>()[i + 1];
        }
        for (int i = 0; i < votePos.Length; i++)
        {
            votePos[i] = GameObject.Find("VotePos").GetComponentsInChildren<Transform>()[i + 1];
        }

        myNum = (int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"];
        waitTime = 7;

        if (PV.IsMine)
        {
            transform.position = new Vector3(40, 44.5f, 0);
            transform.rotation = Quaternion.Euler(0, 90, 0);
            StartCoroutine(StartMove_co());
        }
    }
    IEnumerator StartMove_co()
    {
        yield return new WaitForSeconds(waitTime);

        Vector3 conR = new Vector3();
        Vector3 votR = new Vector3();
        Vector3 conP = new Vector3();

        Quaternion pos = new Quaternion();
        Quaternion pos2 = new Quaternion();

        if (myNum < 4)
        {
            conR = cornerPos[myNum].position - transform.position;
            conP = cornerPos[myNum].position;
        }
        else
        {
            conR = cornerPos[myNum - 4].position - transform.position;
            conP = cornerPos[myNum - 4].position;
        }

        while (pos == null || pos != transform.rotation)
        {
            pos = transform.rotation;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(conR), Time.deltaTime * rotateSpeed);

            yield return null;
        }
        while (Vector3.Distance(transform.position, conP) > 0.1 && transform.position.x > conP.x)
        {
            transform.position = Vector3.Lerp(transform.position, conP, moveSpeed);

            playerAni.SetFloat("Speed", 1);

            yield return null;
        }
        transform.position = conP;

        votR = votePos[myNum].position - transform.position;

        while (pos2 == null || pos2 != transform.rotation)
        {
            pos2 = transform.rotation;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(votR), Time.deltaTime * rotateSpeed);

            yield return null;
        }
        while (Vector3.Distance(transform.position, votePos[myNum].position) > 0.1 && Mathf.Abs(transform.position.z) < Mathf.Abs(votePos[myNum].position.z))
        {
            transform.position = Vector3.MoveTowards(transform.position, votePos[myNum].position, 0.01f);

            playerAni.SetFloat("Speed", 1);

            yield return null;
        }

        playerAni.SetFloat("Speed", 0);
        transform.position = votePos[myNum].position;
        transform.rotation = votePos[myNum].transform.rotation;
    }
}
