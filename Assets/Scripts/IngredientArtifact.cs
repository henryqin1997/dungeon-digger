using UnityEngine;

public class IngredientArtifact : Artifact
{
    public Ingredient ingredient;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = ingredient.icon;
    }

    override protected void OnPickUp()
    {
        Debug.Log("PickedUp: id=" + ingredient.id.ToString() + ", category=" + ingredient.category.ToString() + ", icon=" + ingredient.icon.name);
    }
}
