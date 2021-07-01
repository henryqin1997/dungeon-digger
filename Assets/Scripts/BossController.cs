using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_CLOUD_SERVICES_ANALYTICS
using UnityEngine.Analytics;
#endif

public class BossController : MonoBehaviour
{
    public static BossController instance;
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

    public BossSequence[] sequences;
    public int currentSequence;

    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UIController.instance.bossHealthSlider.maxValue = maxHealth;
        UIController.instance.bossHealthSlider.value = currentHealth;
        actions = sequences[currentSequence].actions;
        actionCounter = actions[currentAction].actionLength;
    }

    // Update is called once per frame
    void Update()
    {
       if(actionCounter > 0) {
           actionCounter -= Time.deltaTime;
           // handle movement
           moveDirection = Vector2.zero;
           if(actions[currentAction].shouldMove) {
               if(actions[currentAction].shouldChasePlayer && GameObject.FindWithTag("Player") != null) {
                   moveDirection = PlayerMovement.instance.transform.position - transform.position;
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
                    Instantiate(actions[currentAction].itemToShoot, t.position, t.rotation);
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
        currentHealth -= damageAmount;
        UIController.instance.bossHealthSlider.value = currentHealth;
        if(currentHealth <= 0) {
            gameObject.SetActive(false);
            //Instantiate(deathEffect, transform.position, transform.rotation);
            //levelExit.SetActive(true);
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
