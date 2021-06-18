using UnityEngine;

public class Dish : MonoBehaviour
{
    public enum ID
    {
        INVALID_ID,

        HOT_TEA,
        MILK_TEA,
        BEEF_AND_BROCCOLI,
        SHOCKING_UNAGIDON,
        FLAMING_HOT_SANDWICH,
        FRENCH_TOAST,
        SOYSAUCE_EGG,
        FRIED_CHICKEN,
        BREAKFAST_SANDWICH
    }

    public ID     id;
    public Sprite sprite;
}
