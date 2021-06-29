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

//Shooting.ChangeBulletSpeed(int speedChange)
//@param positive / negative int

//WaterBeam.ChangeDamage(int damageChange)
//@param positive / negative int

//WaterBeam.ChangeRange(int rangeChange)
//@param positive / negative int

//For move speed, bullet speed, bullet dmg, bullet range, there is a minimum non - zero value.

public class RecipeConsumption : MonoBehaviour
{
    public void ConsumeHotTea()
    {
        PlayerMovement.instance.ChangeMoveSpeed(1);
        //Shooting.ChangeBulletCooldown(1);
    }
    public void ConsumeMilkTea()
    {
        WaterBeam.ChangeDamage(2);
    }
    public void ConsumeBeefAndBrocolli()
    {
        PlayerMovement.instance.IncreaseHealth(1);
        WaterBeam.ChangeDamage(1);
    }
    public void ConsumeNigiri()
    {
        WaterBeam.instance.ChangeDamage(2);
        Shooting.ChangeBulletSpeed(3);
    }
    public void ConsumeFlamingHotSandwich()
    {
        PlayerMovement.ChangeMoveSpeed(2);
        Shooting.ChangeDamage(1);
        PlayerMovement.IncreaseHealth(1);
        Shooting.ChangeBulletSpeed(1);
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
        Shooting.ChangeBulletCooldown(2);
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
        Shooting.ChangeBulletSpeed(2);
    }
    public void ConsumeCaserolle()
    {
        PlayerMovement.IncreaseHealth(2);
        PlayerMovement.IncreaseShield(2);
        WaterBeam.ChangeDamage(2);
    }
    public void ConsumeFishPorridge()
    {
        Shooting.ChangeBulletSpeed(2);
        PlayerMovement.ChangeMoveSpeed(2);
        Shooting.ChangeDamage(1);
    }
    public void ConsumeRicePudding()
    {
        PlayerMovement.IncreaseHealth(1);
        Shooting.ChangeRange(2);
    }
    public void ConsumeSearedSteak()
    {

    }
}
