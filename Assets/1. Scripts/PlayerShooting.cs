using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPref; // �Ѿ� ������ ����
    public GameObject ShootEffectPref; // ���� ȿ�� ������ ����

    public float shootingInterval = 0.2f; // �Ѿ� �߻� ����
    private float lastShootTime;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Confined;
        lastShootTime = -shootingInterval; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButton(0) && Time.time - lastShootTime > shootingInterval)
            {
                Shoot(); // ���� �߻��ϴ� �Լ� ȣ��
                lastShootTime = Time.time; // ������ �� �߻� �ð� ����
            }
        }
    }

    void Shoot()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        RaycastHit hit;

        Physics.Raycast(ray, out hit);

        if (Physics.Raycast(ray, out hit))
        {
            GameObject shootEffect = Instantiate(ShootEffectPref, hit.point + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal));
            shootEffect.transform.SetParent(hit.transform);
        }

        if (hit.transform.tag == "Enemy")
        {
            hit.transform.SendMessage("Damaged", 10);
        }
    }
}
