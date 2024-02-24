using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //public GameObject bulletPref; // �Ѿ� ������ ����
    public GameObject ShootEffectPref; // ���� ȿ�� ������ ����

    public float shootingInterval = 0.1f; // �Ѿ� �߻� ����
    private float lastShootTime; // ������ �߻� �ð�

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;   // Ŀ�� �Ⱥ��̰�
        Cursor.lockState = CursorLockMode.Confined; // ���� ȭ�� �� ����� ��ױ�

        //�Ѿ� �߻� �ʱ�ȭ �۾�
        lastShootTime = -shootingInterval; // ó���� ��� �߻�



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButton(0) && Time.time - lastShootTime > shootingInterval)
            {
                Shoot(); // ���� �߻��ϴ� �Լ� ȣ��

                lastShootTime = Time.time; 
                // ������ �� �߻� �ð� ����
                //���� �߻��� �Ŀ�, ������ �ð��� lastShootTime�� ����
            }
        }
    }

    void Shoot()
    {
        // ȭ�� ����� �����ϴ� Ray ���� ,  Viewport = ����ȭ��
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        RaycastHit hit;  // Ray�� ���� ��ü�� ��Ƶ� ����

        Physics.Raycast(ray, out hit); // Ray�� �߻��ϰ� Ray�� ���� ��ü�� ���� hit�� ����

        if (Physics.Raycast(ray, out hit))
        {
            // ���� ��ġ��, ���� ǥ���� ������ �Ǵ� ������ �� ȿ�� ������ ����
            // hit.normal : Ray�� ������Ʈ�� �浹�� ���� ����, ������ ��Ÿ���� Vector3���̸� Ray�� �ٴڰ� �浹�ϸ� �ٴڰ� ������ �Ǵ� ������ ����
            GameObject shootEffect = Instantiate(ShootEffectPref, hit.point + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal));



            // �� �ڱ��� ���� ������Ʈ�� �ڽ����� ����
            shootEffect.transform.SetParent(hit.transform);
        }

        if (hit.transform.tag == "Enemy")  // Ray�� ���� ��ü�� ���̶��
        {
            hit.transform.SendMessage("Damaged", 10);
            // SendMessage�� ����Ͽ� ���������� Damaged �Լ� ȣ��

            // ���� ��ü�� �̸� ��� (�ٷ� ���� �� �� �ִ� ������Ʈ��  Transfrom, Rigidbody, Collider�� ����)
            //print(hit.transform.name);
        }
    }
}
