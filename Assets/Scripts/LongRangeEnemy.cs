using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeEnemy : EnemyBody
{
    public GameObject bulletToFire;

    public Transform firePoint;

    private float shotCounter;

    public float fireRate;



    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(GameObject.FindWithTag("Player") == null) {
            return;
        }
        Vector3 playerLocation = theCam.WorldToScreenPoint(
            GameObject.FindWithTag("Player").transform.position
          );

        Vector3 enemyLocation = theCam.WorldToScreenPoint(
          transform.localPosition
        );

        if (playerLocation.x > enemyLocation.x)
        {
            attackArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            attackArm.localScale = Vector3.one;
        }

        // rotate arm
        Vector2 offset = new Vector2(enemyLocation.x - playerLocation.x , enemyLocation.y - playerLocation.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        attackArm.rotation = Quaternion.Euler(0, 0, angle);

        // initialize bullet
        if (shouldAttack)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = fireRate;
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
            }
        }
    }


}

