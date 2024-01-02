using System;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
                                    // use when have 2-3 function similar
public interface IItem      
{
    void Destroy();
    string GetDescription();
}
public interface IActiveAbility
{
    float CastDelay { get; }
    float MaxUseRange { get; }
    float CurrentCoolDown { get; }
    float GetCoolDownTime();
    bool IsEnoughMana();
    bool IsInstantCast();
    bool IsReady();
    Respond TryUse();

}
public interface ISpell
{
    void KickOff(ActiveAbility ability, Vector2 direction);
}
public interface IHotKey
{
    void UpdateCoolDown();
    bool IsHotKeyCoolDown(int numkey);
    void UseHotKey(int numkey);
}