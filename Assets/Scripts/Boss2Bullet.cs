using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Bullet : MonoBehaviour
{
    public float speed;
    private Vector3 direction;

    public int damage = 3;
    public static float destructTime = 1.75f;
    public GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        direction = transform.right;
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
        return (boss != null) && boss.activeSelf;
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
