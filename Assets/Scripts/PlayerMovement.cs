using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_CLOUD_SERVICES_ANALYTICS
using UnityEngine.Analytics;
#endif

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    
    public Rigidbody2D rb;
    public Camera cam;
    public GameObject gameover;

    public static int moveSpeed = 10;
    public static int health = 10;
    public static int maxHealth = 10;
    public static int shield = 0;

    Vector2 movement;
    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {

      UIController.instance.healthSlider.maxValue = maxHealth;
      UIController.instance.healthSlider.value = health;
      UIController.instance.healthText.text = health.ToString() + " / " + maxHealth.ToString();
        
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
          Analytics.CustomEvent("gameOver", new Dictionary<string, object>
          {
              { "survive time", Time.time }
          });
          gameover.SetActive(true);
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

    public static void ChangeMoveSpeed(int speedChange)
    {
      moveSpeed += speedChange;
      if (moveSpeed < 10)
      {
        moveSpeed = 10;
      }
    }

    public static void DecreaseHealth(int healthDecrease)
    {
      if (shield >= healthDecrease)
      {
        shield -= healthDecrease;
      }
      else
      {
        shield = 0;
        health -= (healthDecrease - shield);
        if(health < 0) {
          health = 0;
        }
      }
      UIController.instance.healthSlider.value = health;
      UIController.instance.healthText.text = health.ToString() + " / " + maxHealth.ToString();
    }

    public static void IncreaseHealth(int healthIncrease)
    {
      health += healthIncrease;
      UIController.instance.healthSlider.value = health;
      UIController.instance.healthText.text = health.ToString() + " / " + maxHealth.ToString();
    }

    public static void IncreseShield(int shieldIncrease)
    {
      shield += shieldIncrease;
    }
}
