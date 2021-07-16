using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKnife : MonoBehaviour
{
   // Start is called before the first frame update

    public float speed;
    private Vector3 direction;

    public int damage = 3;
    public bool shouldFollowPlayer;
    public static float destructTime = 1f;
    private GameObject boss;

    void Start()
    {
        // why in start? want the bullet to travel in the straight line 
        if(shouldFollowPlayer) {
            GameObject player = GameObject.FindWithTag("Player");
            if (player == null)
        {
            return;
        }
            direction = player.transform.position - transform.position;

        } else {
            direction = transform.right;
        }
        boss = GameObject.FindWithTag("Boss");
        Debug.Assert(boss != null);
        Destroy (gameObject, destructTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        if(!IsBossActive()) {
            Destroy(gameObject);
        }
    }

    private bool IsBossActive()
    {
        if(boss == null) {
            return false;
        }
        return boss.activeSelf;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Player") {
            GetPlayerMovement(collision).DecreaseHealth(damage);
            Destroy(gameObject);
        }
    }

    private static PlayerMovement GetPlayerMovement(Collision2D playerCollision)
    {
        GameObject player = playerCollision.gameObject;
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        Debug.Assert(playerMovement != null);
        return playerMovement;
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
