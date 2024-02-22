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
    public float runSpeed; // �޸��� �ӵ�


    Rigidbody rb; // �÷��̾��� ������ �ٵ� ������Ʈ

    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾��� ������ �ٵ� ������Ʈ ����
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Input �Ŵ����� GetAxis �Լ� ���
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        float speedModifier = 1f;

        // Shift Ű�� ������ ���� ���� �̵� �ӵ��� ����
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedModifier = 2f; // �̵� �ӵ� ����
        }

        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();
        dir = transform.TransformDirection(dir);

        // �����ۿ� �̵�
        rb.MovePosition(rb.position + dir * moveSpeed * speedModifier * Time.deltaTime);


        // Space Ű ������
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jumpCount++;
        }

        // ���콺�� �¿� ������ �Է��� ���ڷ� ����
        float mouseMoveX = Input.GetAxis("Mouse X");

        // ���콺�� ������ ��ŭ Y�� ȸ��
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
