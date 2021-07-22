using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider healthSlider;
    public Slider bossHealthSlider;
    public Text bossHealthText;
    public Text healthText;
    public Sprite shieldSprite;

    private float xMin = 0.03f;
    private float xMax = 0.035f;

    public List<Image> shields = new List<Image>();

    private void Awake()
    {
        bossHealthText = bossHealthSlider.transform.Find("Text").GetComponent<Text>();
    }

    public void OnPlayerHealthUpdated(int newHealth)
    {
        UpdateHealth(healthSlider, newHealth);
        UpdateHealthText();
    }

    public void OnPlayerMaxHeathUpdated(int newMaxHealth)
    {
        UpdateMaxHealth(healthSlider, newMaxHealth);
        UpdateHealthText();
    }

    public void OnPlayerShieldUpdated(int newShield)
    {
        Action updateShields = IncreaseShield;
        if (newShield < shields.Count)
        {
            updateShields = DecreaseShield;
        }

        while (shields.Count != newShield) {
            updateShields();
        }
    }

    public void OnBossHealthUpdated(int newHealth)
    {
        UpdateHealth(bossHealthSlider, newHealth);
    }

    public void OnBossMaxHealthUpdated(int newMaxHealth)
    {
        UpdateMaxHealth(bossHealthSlider, newMaxHealth);
    }

    public void SetBossHealthBarName(string bossName)
    {
        bossHealthText.text = bossName;
    }

    private static void UpdateHealth(Slider healthSlider, int newHealth)
    {
        Debug.Assert(newHealth >= 0);
        Debug.Assert(newHealth <= healthSlider.maxValue);

        healthSlider.value = newHealth;
    }

    private static void UpdateMaxHealth(Slider healthSlider, int newMaxHealth)
    {
        Debug.Assert(newMaxHealth >= 1);

        healthSlider.maxValue = newMaxHealth;
        healthSlider.value    = Math.Min(healthSlider.value, healthSlider.maxValue);
    }

    private void UpdateHealthText()
    {
        healthText.text = healthSlider.value.ToString() + " / " + healthSlider.maxValue.ToString();
    }

    public void IncreaseShield() {
        Image shield = new GameObject().AddComponent<Image>();
        shield.sprite = shieldSprite;
        shield.transform.SetParent(transform, false);
        shield.GetComponent<RectTransform>().anchorMin = new Vector2(xMin, 0.845f);
        shield.GetComponent<RectTransform>().anchorMax = new Vector2(xMax, 0.85f);
        shield.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0f);
        xMin += 0.05f;
        xMax += 0.05f;
        shields.Add(shield);
    }

    public void DecreaseShield() {
        Image shield = shields[shields.Count - 1];
        Vector2 shieldPosition = shield.GetComponent<RectTransform>().anchoredPosition;
        xMin = shield.GetComponent<RectTransform>().anchorMin.x;
        xMax = shield.GetComponent<RectTransform>().anchorMax.x;
        shields.RemoveAt(shields.Count - 1);
        Destroy(shield);
    }
}
