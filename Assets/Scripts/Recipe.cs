using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    public Ingredient[] ingredients;

    // Start is called before the first frame update
    void Start()
    {
        int slot = 0;

        // show assigned ingredients
        for (; slot < ingredients.Length; ++slot) {
            Ingredient ingredient = ingredients[slot];
            var ingredientSlot = GetIngredientSlot(slot);
            var ingredientIcon = ingredientSlot.transform.GetChild(0).gameObject
                                               .transform.GetChild(0).gameObject;
            ingredientIcon.GetComponent<Image>().sprite = ingredient.sprite;
        }

        // hide unused ingredient slots
        for (; slot < gameObject.transform.childCount; ++slot) {
            var ingredientSlot = GetIngredientSlot(slot);
            Destroy(ingredientSlot);
            var plusSign = GetChildTransform("PlusIcon", slot-1);
            if (plusSign != null) {
                Destroy(plusSign.gameObject);
            }
        }
    }

    private GameObject GetIngredientSlot(int slot)
    {
        Transform ingredientSlotTransform = GetChildTransform("IngredientSlot", slot);
        Debug.Assert(ingredientSlotTransform != null);
        return ingredientSlotTransform.gameObject;
    }

    private Transform GetChildTransform(string baseName, int slot)
    {
        return GetChildTransform(baseName + slot.ToString());
    }

    private Transform GetChildTransform(string childName)
    {
        return gameObject.transform.Find(childName);
    }
}
