using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{

    public Rigidbody2D theRB;
    public float moveSpeed;
    public float rangeToChasePlayer;
    public Vector3 moveDirection; // the player direction relative to the enemy
    public bool shouldAttack = true;
    public int health = 1;
    public int shield = 0;
    public Transform attackArm;
    protected Camera theCam;

    public Animator anim;

    public AudioSource audioSource; // TODO: what will this be used for?

    
    // Start is called before the first frame update
    public virtual void Start()
    {
        theCam = Camera.main;
        audioSource = GetComponent<AudioSource>(); // TODO: add AudioSource component to enemy prefabs
    }

    // Update is called once per frame
    public virtual void Update()
    {
        GameObject player = FindPlayer();
        if (player == null)
        {
            return;
        }

        Vector3 playerPosition = player.transform.position;
        bool isPlayerInRange = Vector3.Distance(transform.position, playerPosition) < rangeToChasePlayer;
        shouldAttack = isPlayerInRange;
        // control if the enemy should chase the player
        if (isPlayerInRange)
        {
            moveDirection = playerPosition - transform.position;

        }
        else
        {
            moveDirection = Vector3.zero;
        }
        moveDirection.Normalize();
        theRB.velocity = moveDirection * moveSpeed;

        // change transform depending on player location
        Vector3 playerLocation = theCam.WorldToScreenPoint(
            playerPosition
          );

        Vector3 enemyLocation = theCam.WorldToScreenPoint(
          transform.localPosition
        );
        if (playerLocation.x > enemyLocation.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);

        }
        else
        {
            transform.localScale = Vector3.one;

        }
        // if (moveDirection != Vector3.zero)
        // {
        //     anim.SetBool("isMoving", true);
        // }
        // else
        // {
        //     anim.SetBool("isMoving", false);
        // }
    }

    private GameObject FindPlayer()
    {
        return GameObject.FindWithTag("Player");
    }


    public void DamageEnemy(int damage)
    {
        if (shield > 0)
        {
            shield -= damage;
        }
        else
        {
            health -= damage;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
