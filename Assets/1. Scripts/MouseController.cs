using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float rotateSpeed; // 회전 속도
    float tempX; // eulerAngles.x 변수


    // Update is called once per frame
    void Update()
    {
        float mouseMoveY = Input.GetAxis("Mouse Y");

        transform.Rotate(-mouseMoveY * rotateSpeed * Time.deltaTime, 0, 0);

        if (transform.eulerAngles.x > 180)
        {
            // 360을 빼서 음수로 저장
            tempX = transform.eulerAngles.x - 360;
        }
        else // 180을 넘지 않는다면
        {
            tempX = transform.eulerAngles.x; // 그대로 저장
        }

        // 상하 각도 제한
        tempX = Mathf.Clamp(tempX, -30, 30);
        transform.eulerAngles = new Vector3(tempX, transform.eulerAngles.y, transform.eulerAngles.z);
    }


}
