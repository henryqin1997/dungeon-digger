using UnityEngine;

[System.Serializable]
public class Dish : IItem, IConsumable
{
    public enum ID
    {
        INVALID_ID,

        HOT_TEA,
        MILK_TEA,
        BEEF_AND_BROCCOLI,
        NIGIRI,
        FLAMING_HOT_SANDWICH,
        FRIED_CHICKEN,
        BREAKFAST_SANDIWCH,
        SPICY_FRIED_RICE,
        DONUT,
        CHICKEN_SOUP,
        CASSEROLE,
        FISH_PORRIDGE,
        RICE_PUDDING,
        SEARED_STEAK
    }

    public enum STATUS
    {
        powerful,
        dextrous,
        durable,
        agile

    }

    public ID     id;
    public Sprite icon;
    public Sprite frame;
    public string displayName;
    public string effectDescription;
    public string effectSensation;
    public int moveSpeedChange;
    public int attackDamageChange;
    public int attackCooldownChange;
    public int attackRangeChange;
    public int healthChange;
    public int maxHealthChange;
    public int shieldChange;
    public STATUS status;
    public AudioClip consumeAudioClip;

    public Dish(Dish other)
    {
        id    = other.id;
        icon  = other.icon;
        frame = other.frame;
        displayName  = other.displayName;
        effectDescription = other.effectDescription;
        effectSensation = other.effectSensation;
        moveSpeedChange = other.moveSpeedChange;
        attackDamageChange = other.attackDamageChange;
        attackCooldownChange = other.attackCooldownChange;
        attackRangeChange = other.attackRangeChange;
        healthChange = other.healthChange;
        maxHealthChange = other.maxHealthChange;
        shieldChange = other.shieldChange;
        status = other.status;
        consumeAudioClip = other.consumeAudioClip;
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
        return effectDescription;
    }

    public string GetEffectSensation()
    {
        return effectSensation;
    }

    public int GetMoveSpeedChange()
    {
        return moveSpeedChange;
    }

    public int GetAttackDamageChange()
    {
        return attackDamageChange;
    }

    public int GetAttackCooldownChange()
    {
        return attackCooldownChange;
    }

    public int GetAttackRangeChange()
    {
        return attackRangeChange;
    }

    public int GetHealthChange()
    {
        return healthChange;
    }

    public int GetMaxHealthChange()
    {
        return maxHealthChange;
    }

    public int GetShieldChange()
    {
        return shieldChange;
    }

    public AudioClip GetConsumeAudioClip()
    {
        return consumeAudioClip;
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
            && (dish.frame == this.frame)
            && (dish.displayName == this.displayName)
            && (dish.effectDescription == this.effectDescription)
            && (dish.effectSensation == this.effectSensation)
            && (dish.moveSpeedChange == this.moveSpeedChange)
            && (dish.attackDamageChange == this.attackDamageChange)
            && (dish.attackCooldownChange == this.attackCooldownChange)
            && (dish.attackRangeChange == this.attackRangeChange)
            && (dish.healthChange == this.healthChange)
            && (dish.maxHealthChange == this.maxHealthChange)
            && (dish.shieldChange == this.shieldChange)
            && (dish.status == this.status)
            && (dish.consumeAudioClip == this.consumeAudioClip);
    }
}
