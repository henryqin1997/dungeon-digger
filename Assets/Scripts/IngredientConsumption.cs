using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientConsumption : MonoBehaviour
{
    public void ConsumeIngredient(Ingredient ingredient) {
        PlayerMovement.IncreaseHealth(1);
    }
}
