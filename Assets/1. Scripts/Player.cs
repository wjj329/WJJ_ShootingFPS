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
    public float runSpeed; // 달리기 속도


    Rigidbody rb; // 플레이어의 리지드 바디 컴포넌트

    // Start is called before the first frame update
    void Start()
    {
        // 플레이어의 리지드 바디 컴포넌트 저장
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Input 매니저의 GetAxis 함수 사용
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        float speedModifier = 1f;

        // Shift 키를 누르고 있을 때만 이동 속도를 증가
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedModifier = 2f; // 이동 속도 증가
        }

        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();
        dir = transform.TransformDirection(dir);

        // 물리작용 이동
        rb.MovePosition(rb.position + dir * moveSpeed * speedModifier * Time.deltaTime);


        // Space 키 누르면
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jumpCount++;
        }

        // 마우스의 좌우 움직임 입력을 숫자로 저장
        float mouseMoveX = Input.GetAxis("Mouse X");

        // 마우스가 움직인 만큼 Y축 회전
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
