using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public enum ID {
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
        MEATS,
        SPICES_AND_SAUCES,
        VEGETABLES_AND_STARCHES,
        DRINKS
    }

    public ID       id;
    public Category category;
    public Sprite   sprite;
}
