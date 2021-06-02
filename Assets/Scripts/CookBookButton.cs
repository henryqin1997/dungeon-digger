using UnityEngine;
using UnityEngine.UI;

public class CookBookButton : MonoBehaviour
{
    public Sprite closedSprite;
    public Sprite openSprite;

    // Start is called before the first frame update
    void Start()
    {
        SetSprite(closedSprite);
    }

    public void ToggleSprite()
    {
        Sprite nextSprite = (GetImage().sprite == closedSprite)
                          ? openSprite
                          : closedSprite;
        SetSprite(nextSprite);
    }

    private void SetSprite(Sprite sprite)
    {
        GetImage().sprite = sprite;
    }

    private Image GetImage()
    {
        return gameObject.GetComponent<Image>();
    }
}
