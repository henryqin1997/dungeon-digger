using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FridgeOpenedEvent : UnityEvent<Fridge>
{
}

public class Fridge : MonoBehaviour
{
    public IngredientBehaviour[] contents     = new IngredientBehaviour[3];
    public FridgeOpenedEvent     fridgeOpened = new FridgeOpenedEvent();

    private void Awake()
    {
        IngredientBehaviour[] ingredientBehaviours =
            Resources.LoadAll<IngredientBehaviour>("Prefabs/Ingredients");
        Debug.Assert(ingredientBehaviours != null);
        Debug.Assert(ingredientBehaviours.Length > contents.Length); // 1 invalid base Ingredient prefab
        int remIngredients = ingredientBehaviours.Length;
        for (int remSelections = contents.Length; remSelections > 0; )
        {
            int i = Random.Range(0, remIngredients);
            IngredientBehaviour ingredientBehaviour = ingredientBehaviours[i];
            Debug.Assert(ingredientBehaviour != null);
            if (ingredientBehaviour.ingredient.id != Ingredient.ID.INVALID_ID)
            {
                contents[--remSelections] = ingredientBehaviour;
            }
            ingredientBehaviours[i] = ingredientBehaviours[--remIngredients];
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            fridgeOpened.Invoke(this);
        }
    }
}
