using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    
    public Rigidbody2D rb;
    public Camera cam;
    public GameObject gameover;

    public int moveSpeed = 5;
    public const int MAX_HEALTH = 100;
    public const int MAX_SHIELD = 50;
    public int health = MAX_HEALTH;
    public int shield = MAX_SHIELD;

    Vector2 movement;
    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake() 
  {
    instance = this;
  }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (health <= 0)
        {
          Destroy(gameObject);
          gameover.SetActive(true);
          RoomsEnteredTracker.SendRoomsEntered();
        }
    }

    void FixedUpdate()
    {
        // character movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // character rotation
        Vector2 lookDir = mousePos - rb.position;
        float rotAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = rotAngle;
    }

    public void ChangeMoveSpeed(int speedChange)
    {
      moveSpeed += speedChange;
      if (moveSpeed < 3)
      {
        moveSpeed = 3;
      }
    }

    public void DecreaseHealth(int healthDecrease)
    {
      if (shield >= healthDecrease)
      {
        shield -= healthDecrease;
      }
      else
      {
        shield = 0;
        health -= (healthDecrease - shield);
      }
    }

    public void IncreaseHealth(int healthIncrease)
    {
      if ((health + healthIncrease) > MAX_HEALTH)
      {
        health = MAX_HEALTH;
      }
      else {
        health += healthIncrease;
      }
    }

    public void IncreseShield(int shieldIncrease)
    {
      if ((shield + shieldIncrease) > MAX_SHIELD)
      {
        shield = MAX_SHIELD;
      }
      else {
        shield += shieldIncrease;
      }
    }
}
