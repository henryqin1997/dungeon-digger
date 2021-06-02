using UnityEngine;

public class IngredientArtifact : Artifact
{
    public Ingredient ingredient;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = ingredient.sprite;
    }

    override protected void OnPickUp()
    {
        Debug.Log("PickedUp: category=" + ingredient.category.ToString() + ", sprite=" + ingredient.sprite.name);
    }
}
