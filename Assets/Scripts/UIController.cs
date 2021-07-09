using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider healthSlider;
    public Slider bossHealthSlider;
    public Text healthText;
    public Sprite shieldSprite;

    Vector2 currentPosition = new Vector2(-900f, 400f);

    public List<Image> shields = new List<Image>();
    public static UIController instance;
    // Start is called before the first frame update

    private void Awake() {
        instance = this;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseShield() {
        Image shield = new GameObject().AddComponent<Image>();
        shield.sprite = shieldSprite;
        shield.transform.SetParent(transform, false);

        shield.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentPosition.x, currentPosition.y);
        shield.GetComponent<RectTransform>().localScale = new Vector2(0.75f, 0.75f);
        currentPosition.x = currentPosition.x + 70;
        shields.Add(shield);
    }

    public void decreaseShield() {
        Image shield = shields[shields.Count - 1];
        Vector2 shieldPosition = shield.GetComponent<RectTransform>().anchoredPosition;
        currentPosition = new Vector2(shieldPosition.x, shieldPosition.y);
        shields.RemoveAt(shields.Count - 1);
        Destroy(shield);
    }
}
