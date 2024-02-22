using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float moveSpeed; // 이동 속도 
    public float jumpPower; // 점프 힘
    public float jumpCount; // 점프 횟수
    public float rotateSpeed; // 회전 속도



    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();
        dir = transform.TransformDirection(dir);
        rb.MovePosition(rb.position + dir * moveSpeed * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jumpCount++;
        }

        float mouseMoveX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseMoveX * rotateSpeed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter(Collision collision) //충돌 시작 순간 호출
    {
        if (collision.gameObject.tag == "Ground")
        {
            //점프 횟수 초기화
            jumpCount = 0;
        }
    }
}
