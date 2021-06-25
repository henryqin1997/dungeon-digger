using UnityEngine;

[System.Serializable]
public class Dish : IItem
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
    public Sprite icon;
    public Sprite frame;

    public Dish(Dish other)
    {
        id    = other.id;
        icon  = other.icon;
        frame = other.frame;
    }

    public string GetName()
    {
        return id.ToString();
    }

    public Sprite GetIcon()
    {
        return icon;
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
            && (dish.id    == this.id)
            && (dish.icon  == this.icon)
            && (dish.frame == this.frame);
    }
}
