public interface IConsumable
{
    string GetDisplayName();
    string GetEffectDescription();
    string GetEffectSensation();
    int    GetMoveSpeedChange();
    int    GetAttackDamageChange();
    int    GetAttackCooldownChange();
    int    GetAttackRangeChange();
    int    GetHealthChange();
    int    GetMaxHealthChange();
    int    GetShieldChange();
}
