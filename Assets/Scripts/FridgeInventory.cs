using UnityEngine;
using UnityEngine.UI;
using System;

public class FridgeInventory : MonoBehaviour
{
    // Start is called before the first frame update
    public void ShowContents(Ingredient[] contents)
    {
        int slot = 0;

        GameObject ingredientSlots = gameObject.transform.GetChild(0).gameObject;

        // show assigned ingredients
        for (int maxSlotCount = Math.Min(contents.Length, ingredientSlots.transform.childCount);
            slot < maxSlotCount; ++slot)
        {
            var ingredient       = contents[slot];
            var ingredientButton = ingredientSlots.transform.GetChild(slot).gameObject;
            ingredientButton.GetComponent<Ingredient>().Assign(ingredient);
            ingredientButton.GetComponent<IngredientButton>().AssignIngredient(ingredient);
        }

        // hide unused ingredient slots
        for (; slot < ingredientSlots.transform.childCount; ++slot)
        {
            var ingredientSlot = ingredientSlots.transform.GetChild(slot).gameObject;
            Destroy(ingredientSlot);
        }
    }
}

