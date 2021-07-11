using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    private Vector3 direction;
    public static float destructTime = 1.0f;

    public int damage = 5;
    void Start()
    {
        // why in start? want the bullet to travel in the straight line 
        GameObject player = FindPlayer();
        Debug.Assert(player != null);
        direction = player.transform.position - transform.position;
        direction.Normalize();

        Destroy (gameObject, destructTime);
    }

    private static GameObject FindPlayer()
    {
        return GameObject.FindWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            Debug.Assert(playerMovement != null);
            playerMovement.DecreaseHealth(damage);
        }
        Destroy(gameObject);
        
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
