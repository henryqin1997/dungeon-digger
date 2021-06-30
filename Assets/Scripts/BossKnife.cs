using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKnife : MonoBehaviour
{
   // Start is called before the first frame update

    public float speed;
    private Vector3 direction;

    public int damage = 10;
    public static float destructTime = 1.0f;

    void Start()
    {
        // why in start? want the bullet to travel in the straight line 
        direction = transform.right;
        Destroy (gameObject, destructTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        if(!BossController.instance.gameObject.activeInHierarchy) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Player") {
            PlayerMovement.DecreaseHealth(damage);
            Destroy(gameObject);
        }
            
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
