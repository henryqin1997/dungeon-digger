using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    public const int MAX_INGREDIENTS = 3;
    public IngredientBehaviour[] ingredients;
    public DishBehaviour         dish;
    private Dictionary<Ingredient, int>         requiredIngredients  = new Dictionary<Ingredient, int>();
    private Dictionary<Ingredient, int>         availableIngredients = new Dictionary<Ingredient, int>();
    private Dictionary<Ingredient, List<Image>> ingredientImages     = new Dictionary<Ingredient, List<Image>>();
    private Image                               dishImage;
    private Tooltip                             tooltip;

    public void UpdateAvailableIngredients(Dictionary<Ingredient, int> availableIngredientCounts)
    {
        // Debug.Log(DisplayIngredientCounts(availableIngredientCounts));
        availableIngredients = availableIngredientCounts;
        UpdateGUI();
    }

    public void UpdateGUI()
    {
        // Debug.Log(dish.dish.id.ToString() + " - require(" + DisplayIngredientCounts(requiredIngredients) + ") | have(" + DisplayIngredientCounts(availableIngredients) + ")");
        int countAvailableRequiredIngredients = HighlightAvailableRequiredIngredients();
        bool canCreate = (countAvailableRequiredIngredients == ingredients.Length);
        HighlightDish(canCreate);
        MakeSelectable(canCreate);
    }

    public void MakeSelectable(bool selectable)
    {
        GetDescendant("Glow").SetActive(selectable);
        SetButtonInteractive(selectable);
    }

    public void OnPointerEnter()
    {
        Dish d = dish.dish;
        Debug.Assert(tooltip != null);
        tooltip.ShowTooltip(d.GetDisplayName() + ": " + d.GetEffectDescription());
    }

    public void OnPointerExit()
    {
        Debug.Assert(tooltip != null);
        tooltip.HideTooltip();
    }

#if false
    private static string DisplayIngredientCounts(Dictionary<Ingredient, int> ingredientCounts)
    {
        string output = "{";
        foreach (KeyValuePair<Ingredient, int> keyValues in ingredientCounts)
        {
            output += keyValues.Key.id.ToString() + " : " + keyValues.Value + ", ";
        }
        return output.TrimEnd(',', ' ') + "}";
    }
#endif

    private int HighlightAvailableRequiredIngredients()
    {
        int countAvailableRequiredIngredients = 0;
        foreach (KeyValuePair<Ingredient, int> entry in requiredIngredients) {
            Ingredient ingredient = entry.Key;
            int countRequired     = entry.Value;
            availableIngredients.TryGetValue(ingredient, out var countAvailable);

            int countOverlap = Math.Min(countAvailable, countRequired);
            countAvailableRequiredIngredients += countOverlap;

            HighlightIngredient(ingredient, countOverlap);
        }
        return countAvailableRequiredIngredients;
    }

    private void HighlightIngredient(Ingredient ingredient, int countAvailableRequired)
    {
        List<Image> images = ingredientImages[ingredient];
        int slot = 0;
        for (; slot < countAvailableRequired; ++slot)
        {
            FocusImage(images[slot]);
        }
        for (; slot < images.Count; ++slot)
        {
            FadeImage(images[slot]);
        }
    }

    private void HighlightDish(bool canCreate)
    {
        if (canCreate)
        {
            FocusImage(GetDishImage());
        }
        else
        {
            FadeImage(GetDishImage());
        }
    }

    private static void FadeImage(Image image)
    {
        const float FADED_ALPHA = 0.25f;
        SetImageAlpha(image, FADED_ALPHA);
    }

    private static void FocusImage(Image image)
    {
        const float FOCUSED_ALPHA = 1.0f;
        SetImageAlpha(image, FOCUSED_ALPHA);
    }

    private static void SetImageAlpha(Image image, float alpha)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }

    void Awake()
    {
        Debug.Assert(ingredients.Length <= MAX_INGREDIENTS);

        int slot = 0;

        for (; slot < ingredients.Length; ++slot) {
            RegisterIngredient(slot);
        }

        // hide unused ingredient slots
        for (; slot < MAX_INGREDIENTS; ++slot) {
            HideUnusedIngredientSlot(slot);
        }

        GetDishImage().sprite = dish.dish.icon;
        tooltip = FindTooltip();

        UpdateGUI();
    }

    private static Tooltip FindTooltip()
    {
        GameObject tooltipGameObject = GameObject.Find("Tooltip");
        Debug.Assert(tooltipGameObject != null);
        Tooltip tooltip = tooltipGameObject.GetComponent<Tooltip>();
        Debug.Assert(tooltip != null);
        return tooltip;
    }

    private Image GetDishImage()
    {
        if (dishImage == null)
        {
            dishImage = FindDishImage();
        }
        return dishImage;
    }

    private Image FindDishImage()
    {
        Image image = GetDescendant("DishSlot/DishIcon").GetComponent<Image>();
        Debug.Assert(image != null);
        return image;
    }

    private void RegisterIngredient(int slot)
    {
        Ingredient ingredient = ingredients[slot].ingredient;
        var ingredientImage = GetIngredientImage(slot);
        ingredientImage.sprite = ingredient.icon;

        UpdateRequiredIngredientCount(ingredient);
        AddIngredientImage(ingredient, ingredientImage);
    }

    private void HideUnusedIngredientSlot(int slot)
    {
        Destroy(GetIngredientSlot(slot));
        if (slot != 0) {
            Destroy(GetPlusIcon(slot-1));
        }
    }

    private void UpdateRequiredIngredientCount(in Ingredient ingredient)
    {
        requiredIngredients.TryGetValue(ingredient, out var currentCount);
        requiredIngredients[ingredient] = currentCount + 1;
    }

    private void AddIngredientImage(in Ingredient ingredient, Image ingredientImage)
    {
        List<Image> images = null;
        if (!ingredientImages.TryGetValue(ingredient, out images))
        {
            images = ingredientImages[ingredient] = new List<Image>();
        }
        images.Add(ingredientImage);
    }

    private void SetGlow(bool haveGlow)
    {
        GetDescendant("Glow").SetActive(haveGlow);
    }

    private void SetButtonInteractive(bool interactable)
    {
        gameObject.GetComponent<Button>().interactable = interactable;
    }

    private Image GetIngredientImage(int slot)
    {
        var ingredientSlot  = GetIngredientSlot(slot);
        var ingredientIcon  = ingredientSlot.transform.GetChild(0).gameObject;
        var ingredientImage = ingredientIcon.GetComponent<Image>();
        Debug.Assert(ingredientImage != null);
        return ingredientImage;
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
