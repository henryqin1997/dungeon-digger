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
            var ingredientSlot   = gameObject.transform.GetChild(slot).gameObject;
            var ingredientButton = ingredientSlot.transform.GetChild(0).gameObject;
            var ingredientIcon   = ingredientButton.transform.GetChild(0).gameObject;

            ingredientIcon.GetComponent<Image>().sprite = ingredients[slot].sprite;
        }

        // hide unused ingredient slots
        for (; slot < gameObject.transform.childCount; ++slot) {
            var ingredientSlot = gameObject.transform.GetChild(slot).gameObject;
            Destroy(ingredientSlot);
        }
    }
}
