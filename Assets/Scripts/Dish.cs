using UnityEngine;

<<<<<<< HEAD
public class Dish : MonoBehaviour
=======
public class Dish : MonoBehaviour, IItem
>>>>>>> 6dd50dd6bf357cc1988f25a3f1d5510a62268d33
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
<<<<<<< HEAD
=======
    public Sprite frame;

    public string GetName()
    {
        return id.ToString();
    }

    public Sprite GetIcon()
    {
        return sprite;
    }

    public Sprite GetFrame()
    {
        return frame;
    }

    public override int GetHashCode()
    {
        return (int)id;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Dish);
    }

    public bool Equals(Dish dish)
    {
        return (dish != null)
            && (dish.id == this.id)
            && (dish.sprite == this.sprite)
            && (dish.frame == this.frame);
    }
>>>>>>> 6dd50dd6bf357cc1988f25a3f1d5510a62268d33
}
