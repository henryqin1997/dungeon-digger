using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IngredientsAvailableEvent : UnityEvent<Ingredient[]>
{
}

public class Fridge : MonoBehaviour
{
    public Ingredient[]              contents;
    public IngredientsAvailableEvent ingredientsAvailable;
    public UnityEvent                fridgeOpenToggled;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Character")
        {
            ToggleFridgeOpen();
            ingredientsAvailable.Invoke(contents);
        }
    }

    public void ToggleFridgeOpen()
    {
        fridgeOpenToggled.Invoke();
    }
}
