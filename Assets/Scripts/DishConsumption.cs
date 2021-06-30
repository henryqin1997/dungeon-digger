using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//API for player stats:

//PlayerMovement.ChangeMoveSpeed(int speedChange)
//@param positive / negative int

//PlayerMovement.DecreaseHealth(int healthDecrease)
//@param positive int

//PlayerMovement.IncreaseHealth(int healthIncrease)
//@param positive int

//PlayerMovement.IncreseShield(int shieldIncrease)
//@param positive int

//Shooting.ChangeBulletCooldown(int speedChange)
//@param positive / negative int

//WaterBeam.ChangeDamage(int damageChange)
//@param positive / negative int

//WaterBeam.ChangeRange(int rangeChange)
//@param positive / negative int

//For move speed, bullet speed, bullet dmg, bullet range, there is a minimum non - zero value.

public class DishConsumption : MonoBehaviour
{
    public void ConsumeDish(Dish dish)
    {
        Debug.Log("DishConsumption.ConsumeDish(" + dish.id.ToString() + ")");
        switch (dish.id)
        {
            case Dish.ID.HOT_TEA:
                ConsumeHotTea();
                break;

            case Dish.ID.MILK_TEA:
                ConsumeMilkTea();
                break;

            case Dish.ID.BEEF_AND_BROCCOLI:
                ConsumeBeefAndBrocolli();
                break;

            case Dish.ID.NIGIRI:
                ConsumeNigiri();
                break;

            case Dish.ID.FLAMING_HOT_SANDWICH:
                ConsumeFlamingHotSandwich();
                break;

            case Dish.ID.FRIED_CHICKEN:
                ConsumeFriedChicken();
                break;

            case Dish.ID.BREAKFAST_SANDIWCH:
                ConsumeBreakfastSandwich();
                break;

            case Dish.ID.SPICY_FRIED_RICE:
                ConsumeSpicyFriedRice();
                break;

            case Dish.ID.DONUT:
                ConsumeDonut();
                break;

            case Dish.ID.CHICKEN_SOUP:
                ConsumeChickenSoup();
                break;

            case Dish.ID.CASSEROLE:
                ConsumeCaserolle();
                break;

            case Dish.ID.FISH_PORRIDGE:
                ConsumeFishPorridge();
                break;

            case Dish.ID.RICE_PUDDING:
                ConsumeRicePudding();
                break;

            case Dish.ID.SEARED_STEAK:
                ConsumeSearedSteak();
                break;

            default:
                Debug.Assert(false);
                break;
        }
    }

    public void ConsumeHotTea()
    {
        PlayerMovement.ChangeMoveSpeed(1);
        //Shooting.ChangeBulletCooldown(1);
    }
    public void ConsumeMilkTea()
    {
        WaterBeam.ChangeDamage(2);
    }
    public void ConsumeBeefAndBrocolli()
    {
        PlayerMovement.IncreaseHealth(1);
        WaterBeam.ChangeDamage(1);
    }
    public void ConsumeNigiri()
    {
        WaterBeam.ChangeDamage(2);
        Shooting.ChangeBulletCooldown(3);
    }
    public void ConsumeFlamingHotSandwich()
    {
        PlayerMovement.ChangeMoveSpeed(2);
        WaterBeam.ChangeDamage(1);
        PlayerMovement.IncreaseHealth(1);
        Shooting.ChangeBulletCooldown(1);
    }
    public void ConsumeFriedChicken()
    {
        PlayerMovement.IncreaseHealth(2);
    }
    public void ConsumeBreakfastSandwich()
    {
        PlayerMovement.IncreaseHealth(1);
        PlayerMovement.IncreaseShield(2);
    }
    public void ConsumeSpicyFriedRice()
    {
        PlayerMovement.ChangeMoveSpeed(2);
        WaterBeam.ChangeDamage(1);
        //Shooting.ChangeBulletCooldown(2);
        PlayerMovement.IncreaseShield(2);
    }
    public void ConsumeDonut()
    {
        PlayerMovement.IncreaseHealth(1);
        WaterBeam.ChangeDamage(1);
    }
    public void ConsumeChickenSoup()
    {
        WaterBeam.ChangeRange(2);
        Shooting.ChangeBulletCooldown(2);
    }
    public void ConsumeCaserolle()
    {
        PlayerMovement.IncreaseHealth(2);
        PlayerMovement.IncreaseShield(2);
        WaterBeam.ChangeDamage(2);
    }
    public void ConsumeFishPorridge()
    {
        Shooting.ChangeBulletCooldown(2);
        PlayerMovement.ChangeMoveSpeed(2);
        WaterBeam.ChangeDamage(1);
    }
    public void ConsumeRicePudding()
    {
        PlayerMovement.IncreaseHealth(1);
        WaterBeam.ChangeRange(2);
    }
    public void ConsumeSearedSteak()
    {
        PlayerMovement.ChangeMoveSpeed(-1);
        PlayerMovement.IncreaseHealth(1);
        WaterBeam.ChangeDamage(2);
    }
}