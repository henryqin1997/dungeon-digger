using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{

    public Rigidbody2D theRB;
    public float moveSpeed;
    public float rangeToChasePlayer;
    private Vector3 moveDirection; // the player direction relative to the enemy

    public bool shouldShoot;

    public GameObject bullet;

    public Transform firePoint;

    public float fireRate;
    private float fireCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) < rangeToChasePlayer) {
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
    }
}
