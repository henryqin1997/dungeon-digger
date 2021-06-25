using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControler : MonoBehaviour
{

    public Rigidbody2D theRB;
    public float moveSpeed;
    public float rangeToChasePlayer;
    private Vector3 moveDirection; // the player direction relative to the enemy
    public Slider cookProgressSlider;

    public bool shouldShoot;

    public GameObject bullet;

    public Transform firePoint;

    public Transform cookProgressPoint;
    public int health = 1000;
    public float fireRate;
    private float fireCounter;
    private int currentCookingPorgress = 0;

    // Start is called before the first frame update
    void Start()
    {
        cookProgressSlider.maxValue = 1000;
        cookProgressSlider.value = currentCookingPorgress;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindWithTag("Player") != null && Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) < rangeToChasePlayer) {
            moveDirection = PlayerMovement.instance.transform.position - transform.position;
            shouldShoot = true;

        } else {
            moveDirection = Vector3.zero;
            shouldShoot = false;
        }
        moveDirection.Normalize();
        theRB.velocity = moveDirection * moveSpeed;



      if(shouldShoot) {
          fireCounter -= Time.deltaTime;
          if(fireCounter <= 0) {
              fireCounter = fireRate;
              Instantiate(bullet, firePoint.position, firePoint.rotation);
          }
      }

      cookProgressSlider.transform.position = cookProgressPoint.position;
      currentCookingPorgress++;
      cookProgressSlider.value = currentCookingPorgress;

      
    }

    public void DamageEnemy(int damage) 
     {
         health -= damage;
         if(health <= 0) {
             Destroy(gameObject);
         }
     }
}
