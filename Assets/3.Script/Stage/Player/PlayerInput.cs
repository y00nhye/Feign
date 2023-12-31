using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //input 키 입력 string 값
    private string moveAxisName = "Vertical";
    private string rotateAxisName = "Horizontal";

    public float move { get; private set; }
    public float rotate { get; private set; }

    private void Update()
    {
        //플레이어의 움직임을 멈추는 값 설정 여기서 하기
        //move = 0 / rotate = 0

        move = Input.GetAxis(moveAxisName);
        rotate = Input.GetAxis(rotateAxisName);
    }
}
