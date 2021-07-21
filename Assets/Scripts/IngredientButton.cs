using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class IngredientButton : MonoBehaviour
{
    public IngredientAvailableEvent ingredientAvailableEvent;
    public IngredientAvailableEvent ingredientFocusedEvent;
    public UnityEvent               ingredientDismissedEvent;
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

    public void OnPointerEnter()
    {
        Debug.Assert(ingredient != null);
        ingredientFocusedEvent.Invoke(new Ingredient(ingredient));
    }

    public void OnPointerExit()
    {
        ingredientDismissedEvent.Invoke();
    }
}
