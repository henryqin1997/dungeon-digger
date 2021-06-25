<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
=======
>>>>>>> 6dd50dd6bf357cc1988f25a3f1d5510a62268d33
using UnityEngine;
using UnityEngine.UI;

public class IngredientButton : MonoBehaviour
{
<<<<<<< HEAD
    Ingredient ingredient;

    public void AssignIngredient(Ingredient assignedIngredient)
    {
        ingredient         = assignedIngredient;
=======
    public void AssignIngredient(Ingredient assignedIngredient)
    {
>>>>>>> 6dd50dd6bf357cc1988f25a3f1d5510a62268d33
        var ingredientIcon = gameObject.transform.GetChild(0).gameObject;
        ingredientIcon.GetComponent<Image>().sprite = assignedIngredient.sprite;
    }
}
