using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Shop Item", menuName = "Shop/Potion Stat")]
public class StatsPotion : Consumable
{
    public StatsType statsType;
    public float statsBoots;
    public override void Use()
    {
        base.Use();
        switch(statsType)
        {
            case StatsType.HP:
                PartyController.player.gameObject.GetComponent<Player>().playerdata.basicStats.health += statsBoots;
                break;
            case StatsType.MP:
                PartyController.player.gameObject.GetComponent<Player>().playerdata.basicStats.mana += statsBoots;
                break;
            case StatsType.ATK:
                PartyController.player.gameObject.GetComponent<Player>().playerdata.basicAttack.wandDamage += statsBoots;
                break;
            case StatsType.DEF:
                PartyController.player.gameObject.GetComponent<Player>().playerdata.otherStats.damageReduction += (int)statsBoots;
                break;
            case StatsType.CRIT:
                PartyController.player.gameObject.GetComponent<Player>().playerdata.basicAttack.critChance += statsBoots;
                break;
            case StatsType.CRITDMG:
                PartyController.player.gameObject.GetComponent<Player>().playerdata.basicAttack.maxCritDamage += statsBoots;
                PartyController.player.gameObject.GetComponent<Player>().playerdata.basicAttack.minCritDamage += statsBoots;
                break;
            default:
                break;
        }
    }
}