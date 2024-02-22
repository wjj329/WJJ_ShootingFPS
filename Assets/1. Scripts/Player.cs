using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float moveSpeed; // �̵� �ӵ� 
    public float jumpPower; // ���� ��
    public float jumpCount; // ���� Ƚ��
    public float rotateSpeed; // ȸ�� �ӵ�



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

    private void OnCollisionEnter(Collision collision) //�浹 ���� ���� ȣ��
    {
        if (collision.gameObject.tag == "Ground")
        {
            //���� Ƚ�� �ʱ�ȭ
            jumpCount = 0;
        }
    }
}
