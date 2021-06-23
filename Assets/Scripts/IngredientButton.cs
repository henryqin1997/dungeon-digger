using UnityEngine;
using UnityEngine.UI;

public class IngredientButton : MonoBehaviour
{
    public void AssignIngredient(Ingredient assignedIngredient)
    {
        var ingredientIcon = gameObject.transform.GetChild(0).gameObject;
        ingredientIcon.GetComponent<Image>().sprite = assignedIngredient.sprite;
    }
}
