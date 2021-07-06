using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform waterGun;
    public GameObject beamPrefab;
    public int damageBonus = 0;
    public int rangeBonus = 0;
    public float coolDown = 0.2f;
    public int beamForce = 5;
    public AudioSource audioSource; // TODO: add component

    public static int beamForce = 5;
    public static float coolDown = 0.2f;
    bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

    public void OnConsumableConsumed(IConsumable consumable)
    {
        damageBonus += consumable.GetAttackDamageChange();
        rangeBonus  += consumable.GetAttackRangeChange();
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
        WaterBeam wb = beam.GetComponent<WaterBeam>();
        wb.ChangeDamage(damageBonus);
        wb.ChangeRange( rangeBonus);
        audioSource.Play();
    }

    private void ChangeBulletCooldown(int speedChange)
    {
        coolDown -= speedChange * 0.02f;
    }
}
