using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPref; // ÃÑ¾Ë ÇÁ¸®ÆÕ º¯¼ö
    public GameObject ShootEffectPref; // ½´ÆÃ È¿°ú ÇÁ¸®ÆÕ º¯¼ö

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Confined; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
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
}
