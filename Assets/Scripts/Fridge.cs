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
    public UnityEvent                fridgeOpened;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Character")
        {
            OpenFridge();
        }
    }

    public void OpenFridge()
    {
        ingredientsAvailable.Invoke(contents);
        fridgeOpened.Invoke();
    }
}
