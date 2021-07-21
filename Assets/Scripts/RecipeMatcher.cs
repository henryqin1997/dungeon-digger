using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeMatcher : MonoBehaviour
{
    private Dictionary<Ingredient, int>          availableIngredients = new Dictionary<Ingredient, int>();
    private Dictionary<Ingredient, List<Recipe>> recipesByIngredient  = new Dictionary<Ingredient, List<Recipe>>();

    public void UpdateAvailableIngredients(Dictionary<Ingredient, int> availableIngredientCounts)
    {
        availableIngredients = availableIngredientCounts;
    }

    public void ShowMatchesForIngredient(Ingredient ingredient)
    {
        ClearMatches();
        if (recipesByIngredient.TryGetValue(ingredient, out var matches))
        {
            foreach (Recipe match in matches)
            {
                Recipe recipe = Instantiate(match, transform);
                recipe.UpdateAvailableIngredients(availableIngredients);
                recipe.MakeSelectable(false);
            }
        }
    }

    public void ClearMatches()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    void Awake()
    {
        LoadRecipes();
    }

    void OnDisable()
    {
        ClearMatches();
    }

    private void LoadRecipes()
    {
        foreach (Recipe recipe in Resources.LoadAll<Recipe>("Prefabs/Recipes"))
        {
            RegisterRecipe(recipe);
        }
    }

    private void RegisterRecipe(Recipe recipe)
    {
        HashSet<Ingredient> seen = new HashSet<Ingredient>();
        foreach (IngredientBehaviour ingredientBehaviour in recipe.ingredients)
        {
            Ingredient ingredient = ingredientBehaviour.ingredient;
            if (seen.Add(ingredient))
            {
                if (!recipesByIngredient.TryGetValue(ingredient, out var recipes))
                {
                    recipes = new List<Recipe>();
                    recipesByIngredient.Add(ingredient, recipes);
                }
                recipes.Add(recipe);
            }
        }
    }
}
