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
    public IngredientBehaviour[] contents;
    public FridgeOpenedEvent     fridgeOpened;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            fridgeOpened.Invoke(this);
        }
    }
}
