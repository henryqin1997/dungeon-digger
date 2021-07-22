using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    public IngredientBehaviour[] contents = new IngredientBehaviour[3];
    private FridgeInventory      fridgeInventory;
    private FocusManager         focusManager;

    private void Awake()
    {
        IngredientBehaviour[] ingredientBehaviours =
            Resources.LoadAll<IngredientBehaviour>("Prefabs/Ingredients");
        Debug.Assert(ingredientBehaviours != null);
        Debug.Assert(ingredientBehaviours.Length > contents.Length); // 1 invalid base Ingredient prefab
        int remIngredients = ingredientBehaviours.Length;
        for (int remSelections = contents.Length; remSelections > 0; )
        {
            int i = Random.Range(0, remIngredients);
            IngredientBehaviour ingredientBehaviour = ingredientBehaviours[i];
            Debug.Assert(ingredientBehaviour != null);
            if (ingredientBehaviour.ingredient.id != Ingredient.ID.INVALID_ID)
            {
                contents[--remSelections] = ingredientBehaviour;
            }
            ingredientBehaviours[i] = ingredientBehaviours[--remIngredients];
        }

        fridgeInventory = FindFridgeInventory();
        focusManager    = FindFocusManager();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            FridgeOpened();
        }
    }
    private void FridgeOpened()
    {
        fridgeInventory.ShowContents(this);
        focusManager.AcquireFocus("FridgeInventory");
    }

    private static FridgeInventory FindFridgeInventory()
    {
        bool includeInactive = true;
        var matches = FindObjectsOfType<FridgeInventory>(includeInactive);
        Debug.Assert(matches != null);
        Debug.Assert(matches.Length == 1);
        return matches[0];
    }

    private static FocusManager FindFocusManager()
    {
        bool includeInactive = true;
        var matches = FindObjectsOfType<FocusManager>(includeInactive);
        Debug.Assert(matches != null);
        Debug.Assert(matches.Length == 1);
        return matches[0];
    }
}
