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
        PlayerMovement.ChangeMoveSpeed(4);
        Shooting.ChangeBulletCooldown(4);
    }
    public void ConsumeMilkTea()
    {
        WaterBeam.ChangeDamage(6);
    }
    public void ConsumeBeefAndBrocolli()
    {
        PlayerMovement.IncreaseHealth(4);
        WaterBeam.ChangeDamage(4);
    }
    public void ConsumeNigiri()
    {
        WaterBeam.ChangeDamage(7);
        Shooting.ChangeBulletCooldown(7);
    }
    public void ConsumeFlamingHotSandwich()
    {
        PlayerMovement.ChangeMoveSpeed(2);
        WaterBeam.ChangeDamage(4);
        PlayerMovement.IncreaseHealth(4);
        Shooting.ChangeBulletCooldown(4);
    }
    public void ConsumeFriedChicken()
    {
        PlayerMovement.IncreaseHealth(6);
    }
    public void ConsumeBreakfastSandwich()
    {
        PlayerMovement.IncreaseHealth(4);
        PlayerMovement.IncreaseShield(8);
    }
    public void ConsumeSpicyFriedRice()
    {
        PlayerMovement.ChangeMoveSpeed(2);
        WaterBeam.ChangeDamage(4);
        Shooting.ChangeBulletCooldown(8);
        PlayerMovement.IncreaseShield(4);
    }
    public void ConsumeDonut()
    {
        PlayerMovement.IncreaseHealth(6);
        WaterBeam.ChangeDamage(2);
    }
    public void ConsumeChickenSoup()
    {
        WaterBeam.ChangeRange(6);
        Shooting.ChangeBulletCooldown(6);
    }
    public void ConsumeCaserolle()
    {
        PlayerMovement.IncreaseHealth(6);
        PlayerMovement.IncreaseShield(4);
        WaterBeam.ChangeDamage(6);
    }
    public void ConsumeFishPorridge()
    {
        Shooting.ChangeBulletCooldown(6);
        PlayerMovement.ChangeMoveSpeed(2);
        WaterBeam.ChangeDamage(6);
    }
    public void ConsumeRicePudding()
    {
        PlayerMovement.IncreaseHealth(5);
        WaterBeam.ChangeRange(4);
    }
    public void ConsumeSearedSteak()
    {
        PlayerMovement.ChangeMoveSpeed(-2);
        PlayerMovement.IncreaseHealth(5);
        WaterBeam.ChangeDamage(5);
    }
}