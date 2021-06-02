using UnityEngine;
using UnityEngine.UI;

public class FridgeInventory : MonoBehaviour
{
    public Ingredient[] ingredients;

    // Start is called before the first frame update
    void Start()
    {
        int slot = 0;

        GameObject ingredientSlots = gameObject.transform.GetChild(0).gameObject;

        // show assigned ingredients
        for (; slot < ingredients.Length; ++slot)
        {
            var ingredientSlot = ingredientSlots.transform.GetChild(slot).gameObject;
            var ingredientButton = ingredientSlot.transform.GetChild(0).gameObject;
            var ingredientIcon = ingredientButton.transform.GetChild(0).gameObject;

            ingredientIcon.GetComponent<Image>().sprite = ingredients[slot].sprite;
        }

        // hide unused ingredient slots
        for (; slot < ingredientSlots.transform.childCount; ++slot)
        {
            var ingredientSlot = ingredientSlots.transform.GetChild(slot).gameObject;
            Destroy(ingredientSlot);
        }
    }
}

