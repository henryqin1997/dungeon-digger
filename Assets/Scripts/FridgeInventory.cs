using UnityEngine;
using UnityEngine.UI;
using System;

public class FridgeInventory : MonoBehaviour
{
    private Fridge focusedFridge = null;

    // Start is called before the first frame update
    public void ShowContents(Fridge fridge)
    {
        focusedFridge = fridge;

        int choice = 0;

        GameObject ingredientChoices = gameObject.transform.GetChild(0).gameObject;

        // show assigned ingredients
        for (int maxSlotCount = Math.Min(focusedFridge.contents.Length,
                                         ingredientChoices.transform.childCount);
            choice < maxSlotCount; ++choice)
        {
            var ingredient       = focusedFridge.contents[choice];
            var ingredientButton = ingredientChoices.transform.GetChild(choice).gameObject;
            ingredientButton.GetComponent<Ingredient>().Assign(ingredient);
            ingredientButton.GetComponent<IngredientButton>().AssignIngredient(ingredient);
            var button = ingredientButton.GetComponent<Button>();
        }

        // hide unused ingredients
        for (; choice < ingredientChoices.transform.childCount; ++choice)
        {
            var ingredientSlot = ingredientChoices.transform.GetChild(choice).gameObject;
            Destroy(ingredientSlot);
        }
    }

    public void EmptyFocusedFridge()
    {
        Debug.Assert(focusedFridge != null);
        Destroy(focusedFridge.gameObject);
    }
}

