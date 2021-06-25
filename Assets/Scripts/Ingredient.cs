using UnityEngine;

[System.Serializable]
public class Ingredient : IItem
{
    public enum ID {
        INVALID_ID,

        BEEF,
        CHICKEN,
        EGG,
        ELECTRIC_EEL,

        BREAD,
        BROCCOLI,
        RICE,
        TOMATO,

        HOT_SAUCE,
        HOT_PEPPER,
        MUSHROOM,

        MILK,
        WATER,
        TEA_BAG
    }

    public enum Category {
        INVALID_CATEGORY,

        MEATS,
        SPICES_AND_SAUCES,
        VEGETABLES_AND_STARCHES,
        DRINKS
    }

    public ID       id;
    public Category category;
    public Sprite   icon;
    public Sprite   frame;

    public Ingredient(Ingredient other)
    {
        Assign(other);
    }

    public void Assign(in Ingredient other)
    {
        id       = other.id;
        category = other.category;
        icon    = other.icon;
        frame    = other.frame;
    }

    public override int GetHashCode()
    {
        return (int) id;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Ingredient);
    }

    public bool Equals(Ingredient ingredient)
    {
        return (ingredient != null)
            && (ingredient.id       == this.id)
            && (ingredient.category == this.category)
            && (ingredient.icon     == this.icon)
            && (ingredient.frame    == this.frame);
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
}
