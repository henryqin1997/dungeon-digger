using UnityEngine;

public class DishBehaviour : ItemBehaviour
{
    public Dish dish;

    public override IItem GetItem()
    {
        return dish;
    }
}
