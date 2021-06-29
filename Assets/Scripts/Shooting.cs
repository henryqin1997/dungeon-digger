using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform waterGun;
    public GameObject beamPrefab;

    public static int beamForce = 5;
    public static float coolDown = 0.2f;
    bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && canShoot)
        {
            Shoot();
            canShoot = false;
            Invoke("CooledDown", coolDown);
        }
    }

    void CooledDown()
    {
        canShoot = true;
    }

    void Shoot()
    {
        GameObject beam = Instantiate(beamPrefab, waterGun.position, waterGun.rotation);
        Rigidbody2D rb = beam.GetComponent<Rigidbody2D>();
        rb.AddForce(waterGun.right * beamForce, ForceMode2D.Impulse);
    }

    public static void ChangeBulletSpeed(int speedChange)
    {
        coolDown -= speedChange * 0.02f;
    }
}
