using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Analytics;

[System.Serializable]
public class IngredientAvailableEvent : UnityEvent<Ingredient>
{
}

[System.Serializable]
public class IngredientsUpdatedEvent : UnityEvent<Dictionary<Ingredient, int>>
{
}

[System.Serializable]
public class ConsumableConsumedEvent : UnityEvent<IConsumable>
{
}

public class Inventory : MonoBehaviour
{
    public Dictionary<Ingredient, int>  ingredientCounts        = new Dictionary<Ingredient, int>();
    public Dictionary<Dish, int>        dishCounts              = new Dictionary<Dish, int>();
    public IngredientsUpdatedEvent      ingredientsUpdatedEvent = new IngredientsUpdatedEvent();
    public ConsumableConsumedEvent      consumableConsumedEvent = new ConsumableConsumedEvent();

    private Dictionary<string, int> ingredientsGathered                    = new Dictionary<string, int>();
    private const string            REPORT_INGREDIENTS_GATHERED_EVENT_NAME = "gameOver-ingredients-gathered";
    private Dictionary<string, int> ingredientsUsed                        = new Dictionary<string, int>();
    private const string            REPORT_INGREDIENTS_USED_EVENT_NAME     = "gameOver-ingredients-used";
    private Dictionary<string, int> ingredientsConsumed                    = new Dictionary<string, int>();
    private const string            REPORT_INGREDIENTS_CONSUMED_EVENT_NAME = "gameOver-ingredients-consumed";
    private Dictionary<string, int> dishesMade                             = new Dictionary<string, int>();
    private const string            REPORT_DISHES_MADE_EVENT_NAME          = "gameOver-dishes-made";
    private Dictionary<string, int> dishesConsumed                         = new Dictionary<string, int>();
    private const string            REPORT_DISHES_CONSUMED_EVENT_NAME      = "gameOver-dishes-consumed";

    public void OnEnable()
    {
        UpdateSlots();
    }

    public void OnGameOver()
    {
        ReportCounts(REPORT_INGREDIENTS_GATHERED_EVENT_NAME, ingredientsGathered);
        ReportCounts(REPORT_INGREDIENTS_USED_EVENT_NAME,     ingredientsUsed);
        ReportCounts(REPORT_DISHES_CONSUMED_EVENT_NAME,      ingredientsConsumed);
        ReportCounts(REPORT_DISHES_MADE_EVENT_NAME,          dishesMade);
        ReportCounts(REPORT_DISHES_CONSUMED_EVENT_NAME,      dishesConsumed);
    }

    private static void ReportCounts(string eventName, Dictionary<string, int> counts)
    {
        Dictionary<string, object> payload = new Dictionary<string, object>();
        foreach (KeyValuePair<string, int> entry in counts)
        {
            payload[entry.Key] = (object) entry.Value;
        }
        Analytics.CustomEvent(eventName, payload);
    }

    private void UpdateSlots()
    {
        int slot = 0;

        foreach (KeyValuePair<Ingredient, int> entry in ingredientCounts)
        {
            InventorySlot inventorySlot = FillSlot(slot++, entry.Key, entry.Value, true);
            inventorySlot.SetSelectCallback(delegate { ConsumeIngredient(entry.Key); });
        }

        foreach (KeyValuePair<Dish, int> entry in dishCounts)
        {
            InventorySlot inventorySlot = FillSlot(slot++, entry.Key, entry.Value, true);
            inventorySlot.SetSelectCallback(delegate { ConsumeDish(entry.Key); });
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
        AddItem(ingredient.id.ToString(),   ingredientsGathered);

        Debug.Assert(ingredientCounts != null);
        ingredientsUpdatedEvent.Invoke(ingredientCounts);
    }

    public void MakeRecipe(Recipe recipe)
    {
        Debug.Assert(recipe != null);

        foreach (var ingredientBehavior in recipe.ingredients) {
            Ingredient ingredient = ingredientBehavior.ingredient;
            RemoveItem(ingredient, ingredientCounts);
            AddItem(  ingredient.id.ToString(), ingredientsUsed);
        }
        Dish dish = recipe.dish.dish;
        AddDish(dish);
        AddItem(dish.id.ToString(), dishesMade);

        ingredientsUpdatedEvent.Invoke(ingredientCounts);
    }

    private void ConsumeIngredient(Ingredient ingredient)
    {
        RemoveItem(ingredient, ingredientCounts);
        AddItem(ingredient.id.ToString(), ingredientsConsumed);
        ConsumeConsumable(ingredient);
    }

    private void ConsumeDish(Dish dish)
    {
        RemoveItem(dish, dishCounts);
        AddItem(dish.id.ToString(), dishesConsumed);
        ConsumeConsumable(dish);
    }

    private void ConsumeConsumable(IConsumable consumable)
    {
        UpdateSlots();
        consumableConsumedEvent.Invoke(consumable);
    }

    public void AddDish(Dish dish)
    {
        AddItem(new Dish(dish), dishCounts);
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

    private InventorySlot FillSlot(int slot, IItem item, int count, bool selectable)
    {
        GameObject child = GetChild(slot);
        InventorySlot inventorySlot = child.GetComponent<InventorySlot>();
        inventorySlot.SetItems(item, count);
        inventorySlot.SetSelectable(selectable);
        child.SetActive(true);
        return inventorySlot;
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
