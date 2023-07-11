using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    //플레이어 스크립트 가져오기
    private PlayerInput playerInput;
    private Animator playerAni;
    private Rigidbody playerRigid;

    //이동 및 회전 속도 수치값 변수
    private float moveSpeed = 3f;
    private float rotateSpeed = 120f;

    //Player PhotonView
    private PhotonView PV;

    //Manager
    private TimeManager timeManager;

    [Header("[0Mint 1Blue 2Purple 3Yellow 4Gray 5Pink 6Orange 7Green]")]
    [SerializeField] Material[] materials;

    [Header("[Player Info]")]
    [SerializeField] Image myColor;
    [SerializeField] Text myNickname;
    [SerializeField] SkinnedMeshRenderer slimeColor;
    [SerializeField] Image myRoleColor;
    [SerializeField] Image myRoleImg;
    [SerializeField] Text myRoleTxt;

    [Header("[Player Number]")]
    public int myNum;

    [Header("[Day, Night Position]")]
    [SerializeField] Transform[] roomPos;
    [SerializeField] Transform[] votePos;

    [Header("[Night Event Obj]")]
    [SerializeField] GameObject sword;
    [SerializeField] GameObject kit;
    [SerializeField] GameObject car;
    //찾아서 세팅할 목록
    private GameObject towel;
    private GameObject paint;

    [Header("[Player Role (set)]")]
    public Role myRole;

    [Header("[Player Status (set)")]
    public bool isPaint = false;
    public bool isClean = false;
    public bool isDie = false;

    private void Awake()
    {
        TryGetComponent(out playerInput);
        TryGetComponent(out playerAni);
        TryGetComponent(out playerRigid);
        TryGetComponent(out PV);

        timeManager = FindObjectOfType<TimeManager>();

        towel = GameObject.Find("Towel");
        paint = GameObject.Find("PaintEffect");
    }
    private void Start()
    {
        roomPos = new Transform[8];
        votePos = new Transform[8];

        for (int i = 0; i < roomPos.Length; i++)
        {
            roomPos[i] = GameObject.Find("Room").GetComponentsInChildren<Transform>()[i + 1];
        }
        for (int i = 0; i < votePos.Length; i++)
        {
            votePos[i] = GameObject.Find("VotePos").GetComponentsInChildren<Transform>()[i + 1];
        }

        if (PV.IsMine)
        {
            myNum = (int)PhotonNetwork.LocalPlayer.CustomProperties["myNum"];

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
        slimeColor.material = materials[GameManager.instance.myColorNum[num]];
    }
    private void Update()
    {
        if (PV.IsMine)
        {
            if (timeManager.nightMove)
            {
                PV.RPC("NightMove", RpcTarget.AllBuffered, myNum);
            }
            if (timeManager.dayMove)
            {
                PV.RPC("DayMove", RpcTarget.AllBuffered, myNum);
            }

            if (timeManager.isNight)
            {
                if (FindObjectOfType<RolePlayingBtn>().isRolePlaying)
                {
                    FindObjectOfType<RolePlayingBtn>().isRolePlaying = false;
                    StartCoroutine(RolePlaying_co());
                }
            }
        }
        //Rotate();
        //Move();
        //
        //if (playerInput.move > 0)
        //{
        //    playerAni.SetFloat("Speed", Mathf.Abs(playerInput.move));
        //}
    }
    IEnumerator RolePlaying_co()
    {
        yield return new WaitForSeconds(1f);

        int[] rolePlaying = FindObjectOfType<RolePlayingBtn>().ActionConvertertoInt(myNum);

        if(rolePlaying != null)
        {
            for (int i = 0; i < rolePlaying.Length; i++)
            {
                switch (rolePlaying[i])
                {
                    case 0:
                        sword.SetActive(true);
                        break;
                    case 1:
                        car.SetActive(true);
                        break;
                    case 2:
                        sword.SetActive(true);
                        break;
                    case 3:
                        isPaint = true;
                        break;
                    case 4:
                        isClean = true;
                        break;
                    case 5:
                        sword.SetActive(true);
                        break;
                    case 6:
                        kit.SetActive(true);
                        break;
                }
                yield return new WaitForSeconds(3f);
            }
        }
        FindObjectOfType<RolePlayingBtn>().RolePlayingReset();
        PV.RPC("RolePlayingEnd", RpcTarget.AllBuffered);
    }
    [PunRPC]
    private void RolePlayingEnd()
    {
        FindObjectOfType<RolePlayingBtn>().rolePlayingEnd++;
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
    [PunRPC]
    private void NightMove(int num)
    {
        timeManager.nightMove = false;
        StartCoroutine(NightMove_co(num));
    }

    IEnumerator NightMove_co(int num)
    {
        Vector3 nightR = new Vector3();
        Quaternion pos = new Quaternion();

        nightR = roomPos[num].position - transform.position;

        while (pos == null || pos != transform.rotation)
        {
            pos = transform.rotation;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(nightR), Time.deltaTime * rotateSpeed);

            yield return null;
        }
        while (Vector3.Distance(transform.position, roomPos[num].position) > 0.1 && Mathf.Abs(transform.position.z) < Mathf.Abs(roomPos[num].position.z))
        {
            transform.position = Vector3.MoveTowards(transform.position, roomPos[num].position, 0.005f);

            playerAni.SetFloat("Speed", 1);

            yield return null;
        }

        playerAni.SetFloat("Speed", 0);
        transform.position = roomPos[num].position;
        transform.rotation = roomPos[num].transform.rotation;
    }
    [PunRPC]
    private void DayMove(int num)
    {
        timeManager.dayMove = false;

        transform.position = votePos[num].position;
        transform.rotation = votePos[num].transform.rotation;
    }

}
