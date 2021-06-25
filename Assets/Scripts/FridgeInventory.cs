using UnityEngine;
using UnityEngine.UI;
using System;

public class FridgeInventory : MonoBehaviour
{
<<<<<<< HEAD
    // Start is called before the first frame update
    public void ShowContents(Ingredient[] contents)
=======
    private Fridge focusedFridge = null;

    // Start is called before the first frame update
    public void ShowContents(Fridge fridge)
>>>>>>> 6dd50dd6bf357cc1988f25a3f1d5510a62268d33
    {
        focusedFridge = fridge;

        int choice = 0;

        GameObject ingredientChoices = gameObject.transform.GetChild(0).gameObject;

        // show assigned ingredients
<<<<<<< HEAD
        for (int maxSlotCount = Math.Min(contents.Length, ingredientSlots.transform.childCount);
            slot < maxSlotCount; ++slot)
        {
            var ingredient       = contents[slot];
            var ingredientButton = ingredientSlots.transform.GetChild(slot).gameObject;
            ingredientButton.GetComponent<Ingredient>().Assign(ingredient);
            ingredientButton.GetComponent<IngredientButton>().AssignIngredient(ingredient);
=======
        for (int maxSlotCount = Math.Min(focusedFridge.contents.Length,
                                         ingredientChoices.transform.childCount);
            choice < maxSlotCount; ++choice)
        {
            var ingredient       = focusedFridge.contents[choice];
            var ingredientButton = ingredientChoices.transform.GetChild(choice).gameObject;
            ingredientButton.GetComponent<Ingredient>().Assign(ingredient);
            ingredientButton.GetComponent<IngredientButton>().AssignIngredient(ingredient);
            var button = ingredientButton.GetComponent<Button>();
>>>>>>> 6dd50dd6bf357cc1988f25a3f1d5510a62268d33
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

