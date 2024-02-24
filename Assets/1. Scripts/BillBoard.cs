using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    void Update()
    {
        // 카메라와 자신이 서로 바라보게 설정 or 카메라가 바라보는 방향대로 내가 바라보는 방향 설정
        transform.forward = Camera.main.transform.forward;
    }
}
