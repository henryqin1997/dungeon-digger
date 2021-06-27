using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class IngredientButton : MonoBehaviour
{
    public IngredientAvailableEvent ingredientAvailableEvent;
    private Ingredient ingredient = null;

    public void AssignIngredient(Ingredient assignedIngredient)
    {
        Debug.Assert(assignedIngredient != null);
        var ingredientIcon = gameObject.transform.GetChild(0).gameObject;
        ingredientIcon.GetComponent<Image>().sprite = assignedIngredient.icon;
        ingredient = new Ingredient(assignedIngredient);
    }

    public void OnClick()
    {
        Debug.Assert(ingredient != null);
        ingredientAvailableEvent.Invoke(new Ingredient(ingredient));
    }
}
