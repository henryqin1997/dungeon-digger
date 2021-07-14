using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss2Controller : MonoBehaviour
{	
	public static Boss2Controller instance;

	public Boss2Action[] actions;
	private int currentAction;
	private float actionCounter;
    private float shotCounter;
    private Vector2 moveDirection;
    public Rigidbody2D theRB;

    public Transform playerTransform;

    private void Awake() 
    {
    	instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
         actionCounter = actions[currentAction].actionLength;
         playerTransform = FindPlayerTransform();

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
               if(actions[currentAction].shouldChasePlayer) {
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
}

[System.Serializable]
public class Boss2Action
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