using UnityEngine;
using UnityEngine.UI;
using System;

public class FridgeInventory : MonoBehaviour
{
    // Start is called before the first frame update
    public void ShowContents(Ingredient[] contents)
    {
        int choice = 0;

        GameObject ingredientChoices = gameObject.transform.GetChild(0).gameObject;

        // show assigned ingredients
        for (int maxSlotCount = Math.Min(contents.Length, ingredientChoices.transform.childCount);
            choice < maxSlotCount; ++choice)
        {
            var ingredient       = contents[choice];
            var ingredientButton = ingredientChoices.transform.GetChild(choice).gameObject;
            ingredientButton.GetComponent<Ingredient>().Assign(ingredient);
            ingredientButton.GetComponent<IngredientButton>().AssignIngredient(ingredient);
        }

        // hide unused ingredients
        for (; choice < ingredientChoices.transform.childCount; ++choice)
        {
            var ingredientSlot = ingredientChoices.transform.GetChild(choice).gameObject;
            Destroy(ingredientSlot);
        }
    }
}

