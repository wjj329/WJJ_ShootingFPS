using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float rotateSpeed; // ȸ�� �ӵ�
    float tempX; // eulerAngles.x ����


    // Update is called once per frame
    void Update()
    {
        float mouseMoveY = Input.GetAxis("Mouse Y");

        transform.Rotate(-mouseMoveY * rotateSpeed * Time.deltaTime, 0, 0);

        if (transform.eulerAngles.x > 180)
        {
            // 360�� ���� ������ ����
            tempX = transform.eulerAngles.x - 360;
        }
        else // 180�� ���� �ʴ´ٸ�
        {
            tempX = transform.eulerAngles.x; // �״�� ����
        }

        // ���� ���� ����
        tempX = Mathf.Clamp(tempX, -30, 30);
        transform.eulerAngles = new Vector3(tempX, transform.eulerAngles.y, transform.eulerAngles.z);
    }


}
