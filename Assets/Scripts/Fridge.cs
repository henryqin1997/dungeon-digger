using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fridge : MonoBehaviour
{
    public IngredientInventory ingredients;
    public UnityEvent          fridgeOpened;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Character")
        {
            fridgeOpened.Invoke();
        }
    }
}
