using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IngredientInventoryUpdatedEvent : UnityEvent<Dictionary<Ingredient, int>>
{
}

public class IngredientInventory : MonoBehaviour
{

    Dictionary<Ingredient, int>     ingredientCounts           = new Dictionary<Ingredient, int>();
    IngredientInventoryUpdatedEvent ingredientInventoryUpdated = new IngredientInventoryUpdatedEvent();

    public void AddIngredient(Ingredient ingredient)
    {
        ingredientCounts.TryGetValue(ingredient, out var currentCount);
        ingredientCounts[ingredient] = currentCount + 1;

        ingredientInventoryUpdated.Invoke(ingredientCounts);
    }

    public void RemoveIngredients(Ingredient[] ingredients)
    {
        foreach (var ingredient in ingredients) {
            if (--ingredientCounts[ingredient] == 0) {
                ingredientCounts.Remove(ingredient);
            }
        }

        ingredientInventoryUpdated.Invoke(ingredientCounts);
    }
}
