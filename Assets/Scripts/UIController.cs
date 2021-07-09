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

    private float xMin = 0.03f;
    private float xMax = 0.035f;

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
