using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //public GameObject bulletPref; // 총알 프리팹 변수
    public GameObject ShootEffectPref; // 슈팅 효과 프리팹 변수

    public float shootingInterval = 0.1f; // 총알 발사 간격
    private float lastShootTime; // 마지막 발사 시간

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;   // 커서 안보이게
        Cursor.lockState = CursorLockMode.Confined; // 게임 화면 못 벗어나게 잠그기

        //총알 발사 초기화 작업
        lastShootTime = -shootingInterval; // 처음에 즉시 발사



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButton(0) && Time.time - lastShootTime > shootingInterval)
            {
                Shoot(); // 총을 발사하는 함수 호출

                lastShootTime = Time.time; 
                // 마지막 총 발사 시간 갱신
                //총을 발사한 후에, 현재의 시간을 lastShootTime에 저장
            }
        }
    }

    void Shoot()
    {
        // 화면 가운데서 시작하는 Ray 생성 ,  Viewport = 게임화면
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        RaycastHit hit;  // Ray에 맞은 물체를 담아둘 변수

        Physics.Raycast(ray, out hit); // Ray를 발사하고 Ray에 맞은 물체는 변수 hit에 저장

        if (Physics.Raycast(ray, out hit))
        {
            // 맞은 위치에, 맞은 표면의 수직이 되는 각도로 총 효과 프리팹 생성
            // hit.normal : Ray와 오브젝트가 충돌한 법선 각도, 방향을 나타내는 Vector3값이며 Ray가 바닥과 충돌하면 바닥과 수직이 되는 각도로 향함
            GameObject shootEffect = Instantiate(ShootEffectPref, hit.point + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal));



            // 총 자국을 맞은 오브젝트의 자식으로 설정
            shootEffect.transform.SetParent(hit.transform);
        }

        if (hit.transform.tag == "Enemy")  // Ray에 맞은 물체가 적이라면
        {
            hit.transform.SendMessage("Damaged", 10);
            // SendMessage를 사용하여 간접적으로 Damaged 함수 호출

            // 맞은 물체의 이름 출력 (바로 접근 할 수 있는 컴포넌트는  Transfrom, Rigidbody, Collider만 가능)
            //print(hit.transform.name);
        }
    }
}
