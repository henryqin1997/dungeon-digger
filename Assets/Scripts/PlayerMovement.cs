using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if ENABLE_CLOUD_SERVICES_ANALYTICS
using UnityEngine.Analytics;
#endif

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public Rigidbody2D rb;
    public Camera cam;
    public GameOverBehaviour gameover;

    public static int moveSpeed = 10;
    public static int health = 10;
    public static int maxHealth = 10;
    public static int shield = 0;

    public static int maxShield = 10;

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
            health = 0;
            Destroy(gameObject);
            gameover.GameOver();
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

    public void OnGameOver()
    {
#if ENABLE_CLOUD_SERVICES_ANALYTICS
        Analytics.CustomEvent(
            "RemainingHealth",
            new Dictionary<string, object> {
                { "HP",  (object) health }
            }
        );
#endif
    }

    public static void ChangeMoveSpeed(int speedChange)
    {
        moveSpeed += speedChange;
        if (moveSpeed < 10)
        {
            moveSpeed = 10;
        }
    }

    public static void takeDamage(int damage)
    {
        int remainingDamage = damage;
        if (shield > 0)
        {
            int damageOnShield = Math.Min(shield, damage);
            DecreaseShield(damageOnShield);
            remainingDamage -= damageOnShield;
        }

        if (remainingDamage > 0)
        {
            health -= remainingDamage;
            health = Math.Max(0, health);
        }
    }

    public static void DecreaseHealth(int healthDecrease)
    {
        int remainingDamage = healthDecrease;
        if (shield > 0)
        {
            int damageOnShield = Math.Min(shield, healthDecrease);
            DecreaseShield(damageOnShield);
            remainingDamage -= damageOnShield;
        }

        if (remainingDamage > 0)
        {
            health -= remainingDamage;
            health = Math.Max(0, health);
            UIController.instance.healthSlider.value = health;
            UIController.instance.healthText.text = health.ToString() + " / " + maxHealth.ToString();

        }
    }

    public static void IncreaseHealth(int healthIncrease)
    {
        health += healthIncrease;
        maxHealth = Math.Max(health, maxHealth);
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = health;
        UIController.instance.healthText.text = health.ToString() + " / " + maxHealth.ToString();
    }

    public static void IncreaseShield(int shieldIncrease)
    {
        shield += shieldIncrease;
        UIController.instance.increaseShield();
    }

    public static void DecreaseShield(int shieldDecrease)
    {
        shield -= shieldDecrease;
        UIController.instance.decreaseShield();
    }
}
