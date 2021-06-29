using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class IngredientAvailableEvent : UnityEvent<Ingredient>
{
}

[System.Serializable]
public class IngredientsUpdatedEvent : UnityEvent<Dictionary<Ingredient, int>>
{
}

public class Inventory : MonoBehaviour
{
    public Dictionary<Ingredient, int>  ingredientCounts        = new Dictionary<Ingredient, int>();
    public Dictionary<Dish, int>        dishCounts              = new Dictionary<Dish, int>();
    public IngredientsUpdatedEvent      ingredientsUpdatedEvent = new IngredientsUpdatedEvent();

    public void OnEnable()
    {
        int slot = 0;

        foreach (KeyValuePair<Ingredient, int> entry in ingredientCounts)
        {
            FillSlot(slot++, entry.Key, entry.Value, false);
        }

        foreach (KeyValuePair<Dish, int> entry in dishCounts)
        {
            FillSlot(slot++, entry.Key, entry.Value, true);
        }

        for (; slot < gameObject.transform.childCount; ++slot)
        {
            ClearSlot(slot);
        }
    }

    public void AddIngredient(Ingredient ingredient)
    {
        Debug.Assert(ingredient != null);

        AddItem(new Ingredient(ingredient), ingredientCounts);

        Debug.Assert(ingredientCounts != null);
        ingredientsUpdatedEvent.Invoke(ingredientCounts);
    }

    public void MakeRecipe(Recipe recipe)
    {
        foreach (var ingredientBehavior in recipe.ingredients) {
            RemoveItem(ingredientBehavior.ingredient, ingredientCounts);
        }
        AddDish(recipe.dish.dish);

        ingredientsUpdatedEvent.Invoke(ingredientCounts);
    }

    public void AddDish(Dish dish)
    {
        AddItem(new Dish(dish), dishCounts);
    }

    public void RemoveDish(Dish dish)
    {
        RemoveItem(dish, dishCounts);
    }

    private static void AddItem<T>(T item, Dictionary<T, int> counts)
    {
        counts.TryGetValue(item, out var currentCount);
        counts[item] = currentCount + 1;
    }

    private static void RemoveItem<T>(T item, Dictionary<T, int> counts)
    {
        if (--counts[item] == 0) {
            counts.Remove(item);
        }
    }

    private void FillSlot(int slot, IItem item, int count, bool selectable)
    {
        GameObject child = GetChild(slot);
        InventorySlot inventorySlot = child.GetComponent<InventorySlot>();
        inventorySlot.SetItems(item, count);
        inventorySlot.SetSelectable(selectable);
        child.SetActive(true);
    }

    private void ClearSlot(int slot)
    {
        GetChild(slot).SetActive(false);
    }

    private GameObject GetChild(int slot)
    {
        return gameObject.transform.GetChild(slot).gameObject;
    }
}
