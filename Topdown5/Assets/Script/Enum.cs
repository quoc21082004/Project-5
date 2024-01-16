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
    ShieldZone,
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
    Scopoion,
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
public enum ItemRarity          // item tyoe
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
}
public enum TargetType
{
    Player,
    Enemy,
}
public enum Respond             // player cast spell
{
    Success,
    NotEnoughMana,
    InCasting,
    InCoolDown,
    CanNotUse,
    NotAllow,
}
public enum AbilityName         // boss ability combat
{
    HandFire,
    SpikeFire,
    LazerFire,
    StormSpell,
    SpawnMonster,
    DamageZone
}
public enum BehaviourSpell      // boss spell lazer
{
    RotateAround,
    ChasingTarget,
}
public enum MusicType           // sound , music
{
    Sfx,
    Bgm,
}