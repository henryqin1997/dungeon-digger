using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    public int damage  = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") {
            GetPlayerMovement(other).DecreaseHealth(damage);
        }
    }

    private static PlayerMovement GetPlayerMovement(Collider2D playerCollision)
    {
        GameObject player = playerCollision.gameObject;
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        Debug.Assert(playerMovement != null);
        return playerMovement;
    }
}
