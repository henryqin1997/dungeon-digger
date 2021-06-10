using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientButton : MonoBehaviour
{
    Ingredient ingredient;

    public void AssignIngredient(Ingredient assignedIngredient)
    {
        ingredient         = assignedIngredient;
        var ingredientIcon = gameObject.transform.GetChild(0).gameObject;
        ingredientIcon.GetComponent<Image>().sprite = assignedIngredient.sprite;
    }
}
