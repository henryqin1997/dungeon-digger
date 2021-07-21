using UnityEngine;

[System.Serializable]
public class Ingredient : IItem, IConsumable
{
    public enum ID {
        INVALID_ID,

        BEEF,
        FISH,
        CHICKEN,
        EGG,

        HOT_PEPPER,
        TEA_BAG,
        OIL,

        VEGETABLES,
        RICE,
        BREAD,

        MILK,
        WATER,
    }

    public enum Category {
        INVALID_CATEGORY,

        MEATS,
        SPICES_AND_SAUCES,
        VEGETABLES_AND_STARCHES,
        DRINKS
    }

    public ID        id;
    public Category  category;
    public Sprite    icon;
    public Sprite    frame;
    public string    displayName;
    public AudioClip consumeAudioClip;

    public Ingredient(Ingredient other)
    {
        Assign(other);
    }

    public void Assign(in Ingredient other)
    {
        id               = other.id;
        category         = other.category;
        icon             = other.icon;
        frame            = other.frame;
        displayName      = other.displayName;
        consumeAudioClip = other.consumeAudioClip;
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
            && (ingredient.id                == this.id)
            && (ingredient.category          == this.category)
            && (ingredient.icon              == this.icon)
            && (ingredient.frame             == this.frame)
            && (ingredient.displayName       == this.displayName)
            && (ingredient.consumeAudioClip  == this.consumeAudioClip);
    }

    public string GetDisplayName()
    {
        return displayName;
    }
    
    public Sprite GetIcon()
    {
        return icon;
    }

    public Sprite GetFrame()
    {
        return frame;
    }

    public string GetEffectDescription()
    {
        return "replenishes some health";
    }

    public string GetEffectSensation()
    {
        return "You feel slightly better.";
    }

    public int GetMoveSpeedChange()
    {
        return 0;
    }

    public int GetAttackDamageChange()
    {
        return 0;
    }

    public int GetAttackCooldownChange()
    {
        return 0;
    }

    public int GetAttackRangeChange()
    {
        return 0;
    }

    public int GetHealthChange()
    {
        return +1;
    }

    public int GetMaxHealthChange()
    {
        return 0;
    }

    public int GetShieldChange()
    {
        return 0;
    }

    public AudioClip GetConsumeAudioClip()
    {
        return consumeAudioClip;
    }
}
