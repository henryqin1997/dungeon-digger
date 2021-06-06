using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    private Vector3 direction;

    void Start()
    {
        // why in start? want the bullet to travel in the straight line 
        direction = PlayerMovement.instance.transform.position - transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(gameObject);

    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
