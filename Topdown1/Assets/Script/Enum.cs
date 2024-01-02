public enum ConsumableType
{
    HealthPotion,
    ManaPotion,
    StatPotion,
    None,
}
public enum PotionType
{
    HEALTH,
    MANA,
}
public enum SpellBookType
{
    ExplosionCircle,
    ExplosionBuilet,
    PoisonZone,
    LightingCircle,
    None,
}
public enum TypeEnemy
{
    Bat,
    Boar,
    Boss,
    FireTotem,
    FlyingMelee,
    LittleRange,
    LittleMelee,
    Skeleton,
}
public enum StatsType
{
    HP,
    MP,
    ATK,
    DEF,
    CRIT,
    CRITDMG,
}
public enum ItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
}
public enum Team
{
    Player,
    Enemy,
}
public enum TargetType
{
    Player,
    Enemy,
}
public enum Respond
{
    Success,
    NotEnoughMana,
    InCasting,
    InCoolDown,
    CanNotUse,
    NotAllow,
}