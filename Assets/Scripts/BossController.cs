using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class BossController : MonoBehaviour
{
    public BossAction[] actions;
    protected int currentAction;
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

    public Transform playerTransform;
    protected Animator anim;

    public GameObject HPCanvas;
    public float rotateSpeed = 0.0f;
    public string bossName;

    public void Start()
    {
        bossActivate();
        playerTransform = FindPlayerTransform();
        HPCanvas.GetComponent<UIController>().OnBossMaxHealthUpdated(maxHealth);
        HPCanvas.GetComponent<UIController>().OnBossHealthUpdated(currentHealth);
        HPCanvas.GetComponent<UIController>().SetBossHealthBarName(bossName);
        actions = sequences[currentSequence].actions;
        actionCounter = actions[currentAction].actionLength;
        anim = GetComponent<Animator>();
        gameOver = Resources.FindObjectsOfTypeAll<GameOverBehaviour>()[0];
    }

    void bossActivate()
    {
      HPCanvas = GameObject.Find("HPCanvas");
      GameObject healthbar = HPCanvas.transform.Find("Boss_Health_Slider").gameObject;
      healthbar.SetActive(true);
      GameObject musiccontroller = GameObject.Find("MusicController");
      AudioClip explode = Resources.Load("Sounds/boss_fight") as AudioClip;
      musiccontroller.GetComponent<MusicController>().PlayMusic(explode);
    }

    void OnEnable()
    {
        
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
    public virtual void Update()
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

               if(actions[currentAction].moveToPoint && Vector3.Distance(transform.position, actions[currentAction].pointToMoveTo.position) > 3f) {
                   moveDirection = actions[currentAction].pointToMoveTo.position - transform.position;
                   moveDirection.Normalize();
               }
           }




           theRB.velocity = moveDirection * actions[currentAction].moveSpeed;

           // handle shooting
           if(actions[currentAction].shouldShoot == true) {
               shotCounter -= Time.deltaTime;
               if(shotCounter <= 0) {
                shotCounter = actions[currentAction].timeBetweenShots;
                    foreach (Transform t in actions[currentAction].shotPoints) {
                    Vector3 axis = new Vector3(0, 0, 1);
                    t.RotateAround(this.transform.position, axis, rotateSpeed * Time.time);
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

    void explodesound()
    {
      GameObject musiccontroller = GameObject.Find("MusicController");
      AudioClip explode = Resources.Load("Sounds/explosion") as AudioClip;
      musiccontroller.GetComponent<MusicController>().PlayMusic(explode);
    }

    public void TakeDamage(int damageAmount) {
        currentHealth = Math.Max(currentHealth - damageAmount, 0);
        HPCanvas.GetComponent<UIController>().OnBossHealthUpdated(currentHealth);
        if(currentHealth <= 0) {
            
            //Instantiate(deathEffect, transform.position, transform.rotation);
            //levelExit.SetActive(true);
            GameObject healthbar = GameObject.Find("Boss_Health_Slider");
            healthbar.SetActive(false);
            this.gameObject.SetActive(false);
  	        GameObject character = FindPlayer();
            character.GetComponent<PlayerMovement>().OnBossDefeated();
            explodesound();
            GameObject levelgenerator = GameObject.Find("level_generator");
            level_generator lg = levelgenerator.GetComponent<level_generator>();
            GameObject musiccontroller = GameObject.Find("MusicController");
            AudioClip explode = Resources.Load("Sounds/music_bg") as AudioClip;
            musiccontroller.GetComponent<MusicController>().PlayMusic(explode);
            if (lg.level>2)
            {
              gameOver.GameOver(true);
            }
            else
            {
              lg.destroy_level();
              lg.generate_level();
            }

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
