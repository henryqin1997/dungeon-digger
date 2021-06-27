using UnityEngine;
using UnityEngine.UI;

public class IngredientArtifact : Artifact
{
    public IngredientBehaviour      ingredient;
    public IngredientAvailableEvent ingredientAvailableEvent;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = GetIngredient().icon;
    }

    override protected void OnPickUp()
    {
        Debug.Log("PickedUp: id=" + GetIngredient().id.ToString() + ", category=" + GetIngredient().category.ToString() + ", icon=" + GetIngredient().icon.name);
        ingredientAvailableEvent.Invoke(GetIngredient());
    }

    private Ingredient GetIngredient()
    {
        return ingredient.ingredient;
    }
}
