using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform waterGun;
    public GameObject beamPrefab;

    public int beamForce = 5;
    private float coolDown = 0.1f;
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

    public void ChangeBulletSpeed(int speedChange)
    {
        beamForce += speedChange;
        if (beamForce < 5)
        {
            beamForce = 5;
        }
    }
}
