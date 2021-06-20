using UnityEngine;
using UnityEngine.UI;

public class ToggleSpriteBehaviour : MonoBehaviour
{
    public Sprite offSprite;
    public Sprite onSprite;

    // Start is called before the first frame update
    void Start()
    {
        SetSprite(offSprite);
    }

    public void ToggleSprite()
    {
        Sprite nextSprite = (GetImage().sprite == offSprite)
                          ? onSprite
                          : offSprite;
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
