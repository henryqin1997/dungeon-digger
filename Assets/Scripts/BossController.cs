using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BossHealthUpdatedEvent : UnityEvent<int>
{}

public class BossController : MonoBehaviour
{
    public BossAction[] actions;
    private int currentAction;
    private float actionCounter;
    private float shotCounter;
    private Vector2 moveDirection;
    public Rigidbody2D theRB;

    public int currentHealth = 50;
    public int maxHealth = 50;
    public GameObject deathEffect;
    public GameObject levelExit;
    public GameOverBehaviour gameOver;
    public UnityEvent bossDefeatedEvent;
    public BossHealthUpdatedEvent bossHealthUpdatedEvent;
    public BossHealthUpdatedEvent bossMaxHealthUpdatedEvent;
    public UnityEvent bossActivatedEvent;

    public BossSequence[] sequences;
    public int currentSequence;

    public Transform playerTransform;

    public void Start()
    {
        playerTransform = FindPlayerTransform();
        bossMaxHealthUpdatedEvent.Invoke(maxHealth);
        bossHealthUpdatedEvent.Invoke(   currentHealth);
        actions = sequences[currentSequence].actions;
        actionCounter = actions[currentAction].actionLength;
    }

    void OnEnable()
    {
        bossActivatedEvent.Invoke();
    }

    private static Transform FindPlayerTransform()
    {
         GameObject character = FindPlayer();
         Debug.Assert(character != null);

         PlayerMovement playerMovement = character.GetComponent<PlayerMovement>();
         Debug.Assert(playerMovement != null);

         return playerMovement.transform;
    }

    private static GameObject FindPlayer()
    {
        return GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       if(actionCounter > 0) {
           actionCounter -= Time.deltaTime;
           // handle movement
           moveDirection = Vector2.zero;
           if(actions[currentAction].shouldMove) {
               if(actions[currentAction].shouldChasePlayer && FindPlayer() != null) {
                   moveDirection = playerTransform.position - transform.position;
                   moveDirection.Normalize();
               }

               if(actions[currentAction].moveToPoint && Vector3.Distance(transform.position, actions[currentAction].pointToMoveTo.position) > .5f) {
                   moveDirection = actions[currentAction].pointToMoveTo.position - transform.position;
                   moveDirection.Normalize();
               }
           }




           theRB.velocity = moveDirection * actions[currentAction].moveSpeed;

           // handle shooting
           if(actions[currentAction].shouldShoot) {
               shotCounter -= Time.deltaTime;
               if(shotCounter <= 0) {
                shotCounter = actions[currentAction].timeBetweenShots;

                foreach(Transform t in actions[currentAction].shotPoints) {
                	// t.Rotate(45.0f, 0.0f, 90.0f, Space.Self);
                    Instantiate(actions[currentAction].itemToShoot, t.position, Quaternion.Euler(new Vector3(0, 0, -90 * Time.time)));
                }
               }
           }

       } else {
           currentAction++;
           if(currentAction >= actions.Length) {
               currentAction = 0;
           }
           actionCounter = actions[currentAction].actionLength;
       }
    }

    public void TakeDamage(int damageAmount) {
        currentHealth = Math.Max(currentHealth - damageAmount, 0);
        bossHealthUpdatedEvent.Invoke(currentHealth);
        if(currentHealth <= 0) {
            gameObject.SetActive(false);
            //Instantiate(deathEffect, transform.position, transform.rotation);
            //levelExit.SetActive(true);
	        bossDefeatedEvent.Invoke();
            gameOver.GameOver();

        } else {
            if(currentHealth <= sequences[currentSequence].endSequenceHealth && currentSequence < sequences.Length - 1) {
                currentSequence ++;
                actions = sequences[currentSequence].actions;
                currentAction = 0;
                actionCounter = actions[currentAction].actionLength;
            }
        }
    }
}

[System.Serializable]
public class BossAction
{
    [Header("Action")]
    public float actionLength;
    public bool shouldMove;
    public bool shouldChasePlayer;
    public bool moveToPoint;
    public float moveSpeed;
    public Transform pointToMoveTo;

    public bool shouldShoot;
    public GameObject itemToShoot;
    public float timeBetweenShots;
    public Transform[] shotPoints;

}

[System.Serializable]
public class BossSequence {
    [Header("Sequence")]
    public BossAction[] actions;
    public int endSequenceHealth;

}
