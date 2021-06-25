using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    private const int MAX_INGREDIENTS = 3;
    public IngredientBehaviour[] ingredients;
    public DishBehaviour         dish;
    private Dictionary<Ingredient, int> ingredientCounts = new Dictionary<Ingredient, int>();

    public void UpdateSelectable(Dictionary<Ingredient, int> availableIngredientCounts)
    {
        Debug.Log(dish.dish.id.ToString() + " - require(" + DisplayIngredientCounts(ingredientCounts) + ") | have(" + DisplayIngredientCounts(availableIngredientCounts) + ")");
        MakeSelectable(CanCreate(availableIngredientCounts));
    }

    private static string DisplayIngredientCounts(Dictionary<Ingredient, int> ingredientCounts)
    {
        string output = "{";
        foreach (KeyValuePair<Ingredient, int> keyValues in ingredientCounts)
        {
            output += keyValues.Key.id.ToString() + " : " + keyValues.Value + ", ";
        }
        return output.TrimEnd(',', ' ') + "}";
    }

    private bool CanCreate(in Dictionary<Ingredient, int> availableIngredientCounts)
    {
        foreach (KeyValuePair<Ingredient, int> entry in ingredientCounts) {
            Ingredient ingredient = entry.Key;
            int countRequired     = entry.Value;
            availableIngredientCounts.TryGetValue(ingredient, out var countAvailable);
            if (countRequired > countAvailable) {
                return false;
            }
        }
        return true;
    }

    public void MakeSelectable(bool selectable)
    {
        GetDescendant("Glow").SetActive(selectable);
        SetButtonInteractive(selectable);
    }

    void Awake()
    {
        Debug.Log("Awake");
        foreach (IngredientBehaviour ingredientBehaviour in ingredients)
        {
            Ingredient ingredient = ingredientBehaviour.ingredient;
            ingredientCounts.TryGetValue(ingredient, out var currentCount);
            ingredientCounts[ingredient] = currentCount + 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        int slot = 0;

        // show assigned ingredients
        for (; slot < ingredients.Length; ++slot) {
            Ingredient ingredient = ingredients[slot].ingredient;
            var ingredientSlot = GetIngredientSlot(slot);
            var ingredientIcon = ingredientSlot.transform.GetChild(0).gameObject;
            ingredientIcon.GetComponent<Image>().sprite = ingredient.icon;
        }

        // hide unused ingredient slots
        for (; slot < MAX_INGREDIENTS; ++slot) {
            Destroy(GetIngredientSlot(slot));
            if (slot != 0) {
                Destroy(GetPlusIcon(slot-1));
            }
        }

        GetDescendant("DishSlot/DishIcon").GetComponent<Image>().sprite = dish.dish.icon;

        MakeSelectable(false);
    }

    private void SetButtonInteractive(bool interactable)
    {
        gameObject.GetComponent<Button>().interactable = interactable;
    }

    private GameObject GetIngredientSlot(int slot)
    {
        return GetDescendant("IngredientSlot", slot);
    }

    private GameObject GetPlusIcon(int slot)
    {
        return GetDescendant("PlusIcon", slot);
    }

    private GameObject GetDescendant(string basePath, int slot)
    {
        return GetDescendant(basePath + slot.ToString());
    }

    private GameObject GetDescendant(string path)
    {
        Transform transform = gameObject.transform.Find(path);
        Debug.Assert(transform != null);
        return transform.gameObject;
    }
}
