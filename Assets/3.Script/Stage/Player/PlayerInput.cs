using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //input Ű �Է� string ��
    private string moveAxisName = "Vertical";
    private string rotateAxisName = "Horizontal";

    public float move { get; private set; }
    public float rotate { get; private set; }

    private void Update()
    {
        //�÷��̾��� �������� ���ߴ� �� ���� ���⼭ �ϱ�
        //move = 0 / rotate = 0

        move = Input.GetAxis(moveAxisName);
        rotate = Input.GetAxis(rotateAxisName);
    }
}
