using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    void Update()
    {
        // ī�޶�� �ڽ��� ���� �ٶ󺸰� ���� or ī�޶� �ٶ󺸴� ������ ���� �ٶ󺸴� ���� ����
        transform.forward = Camera.main.transform.forward;
    }
}
