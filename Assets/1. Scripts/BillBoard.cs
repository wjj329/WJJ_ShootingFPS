using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // ī�޶�� �ڽ��� ���� �ٶ󺸰� ���� or ī�޶� �ٶ󺸴� ������ ���� �ٶ󺸴� ���� ����
        transform.forward = Camera.main.transform.forward;
    }
}
