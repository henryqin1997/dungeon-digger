using UnityEngine;

public class IngredientBehaviour : ItemBehaviour
{
    public Ingredient ingredient;

    public override IItem GetItem()
    {
        return ingredient;
    }
}
