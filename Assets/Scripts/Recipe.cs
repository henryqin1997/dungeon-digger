using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    private const int MAX_INGREDIENTS = 3;
    public Ingredient[] ingredients;
    public Dish         dish;

    // Start is called before the first frame update
    void Start()
    {
        int slot = 0;

        // show assigned ingredients
        for (; slot < ingredients.Length; ++slot) {
            Ingredient ingredient = ingredients[slot];
            var ingredientSlot = GetIngredientSlot(slot);
            var ingredientIcon = ingredientSlot.transform.GetChild(0).gameObject;
            ingredientIcon.GetComponent<Image>().sprite = ingredient.sprite;
        }

        // hide unused ingredient slots
        for (; slot < MAX_INGREDIENTS; ++slot) {
            Destroy(GetIngredientSlot(slot));
            if (slot != 0) {
                Destroy(GetPlusIcon(slot-1));
            }
        }

        GetDescendant("DishSlot/DishIcon").GetComponent<Image>().sprite = dish.sprite;
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
