using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
#if ENABLE_CLOUD_SERVICES_ANALYTICS
using UnityEngine.Analytics;
#endif

[System.Serializable]
public class PlayerStatUpdatedEvent : UnityEvent<int>
{}

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera cam;
    public GameOverBehaviour gameover;

    public PlayerStatUpdatedEvent moveSpeedUpdatedEvent;
    public PlayerStatUpdatedEvent healthUpdatedEvent;
    public PlayerStatUpdatedEvent maxHealthUpdatedEvent;
    public PlayerStatUpdatedEvent shieldUpdatedEvent;

    public int moveSpeed = 10;
    public int health    = 10;
    public int maxHealth = 10;
    public int shield    = 0;
    public int maxShield = 10;

    Vector2 movement;
    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 10;
        health    = 10;
        maxHealth = 10;
        shield    = 0;
        maxShield = 10;

        moveSpeedUpdatedEvent.Invoke(moveSpeed);
        maxHealthUpdatedEvent.Invoke(maxHealth);
        healthUpdatedEvent.Invoke(   health);
        shieldUpdatedEvent.Invoke(   shield);
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

    public void OnConsumableConsumed(IConsumable consumable)
    {
        ChangeMoveSpeed(  consumable.GetMoveSpeedChange());
        IncreaseMaxHealth(consumable.GetMaxHealthChange());
        IncreaseHealth(   consumable.GetHealthChange());
        IncreaseShield(   consumable.GetShieldChange());
    }

    public void OnBossDefeated()
    {
#if ENABLE_CLOUD_SERVICES_ANALYTICS
        Analytics.CustomEvent(
            "bossDefeated",
            new Dictionary<string, object> {
                { "player_remainingHealth",  (object) health },
                { "player_remainingShield",  (object) shield }
            }
        );
#endif
    }

    public void OnGameOver()
    {
#if ENABLE_CLOUD_SERVICES_ANALYTICS
        Analytics.CustomEvent(
            "gameOver",
            new Dictionary<string, object> {
                { "player_maxHealth",  (object) maxHealth },
                { "player_maxShield",  (object) maxShield }
            }
        );
#endif
    }

    public void ChangeMoveSpeed(int speedChange)
    {
        moveSpeed += speedChange;
        if (moveSpeed < 10)
        {
            moveSpeed = 10;
        }
        moveSpeedUpdatedEvent.Invoke(moveSpeed);
    }

    public void DecreaseHealth(int healthDecrease)
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
            healthUpdatedEvent.Invoke(health);
        }
    }

    public void IncreaseHealth(int healthIncrease)
    {
        health = Math.Max(0, Math.Min(health + healthIncrease, maxHealth));
        healthUpdatedEvent.Invoke(health);
    }

    public void IncreaseMaxHealth(int healthIncrease)
    {
        maxHealth += healthIncrease;
        maxHealthUpdatedEvent.Invoke(maxHealth);

        health    += healthIncrease;
        healthUpdatedEvent.Invoke(health);
    }

    public void IncreaseShield(int shieldIncrease)
    {
        shield = Math.Max(0, Math.Min(shield + shieldIncrease, maxShield));
        shieldUpdatedEvent.Invoke(shield);
    }

    public void DecreaseShield(int shieldDecrease)
    {
        shield -= shieldDecrease;
        shieldUpdatedEvent.Invoke(shield);
    }
}
