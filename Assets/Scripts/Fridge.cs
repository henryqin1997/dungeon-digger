using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
<<<<<<< HEAD
public class IngredientsAvailableEvent : UnityEvent<Ingredient[]>
=======
public class FridgeOpenedEvent : UnityEvent<Fridge>
>>>>>>> 6dd50dd6bf357cc1988f25a3f1d5510a62268d33
{
}

public class Fridge : MonoBehaviour
{
<<<<<<< HEAD
    public Ingredient[]              contents;
    public IngredientsAvailableEvent ingredientsAvailable;
    public UnityEvent                fridgeOpenToggled;
=======
    public Ingredient[]      contents;
    public FridgeOpenedEvent fridgeOpened;
>>>>>>> 6dd50dd6bf357cc1988f25a3f1d5510a62268d33

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Character")
        {
<<<<<<< HEAD
            ToggleFridgeOpen();
            ingredientsAvailable.Invoke(contents);
=======
            fridgeOpened.Invoke(this);
>>>>>>> 6dd50dd6bf357cc1988f25a3f1d5510a62268d33
        }
    }

    public void ToggleFridgeOpen()
    {
        fridgeOpenToggled.Invoke();
    }
}
