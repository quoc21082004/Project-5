using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZoneSpellCenter : MonoBehaviour, ISpell
{
    public Transform[] damageZonePos;
    public GameObject damageZoneprefab;
    public void KickOff(ActiveAbility ability, Vector2 direction)
    {
        this.transform.position = transform.position;
        foreach (var pos in damageZonePos)
        {
            if (Instantiate(damageZoneprefab).gameObject.TryGetComponent<DamageZoneSpell>(out DamageZoneSpell spell))
            {
                spell.transform.parent = transform;
                spell.KickOff(ability, pos.transform.position);
            }
        }
        //Destroy(this.gameObject);
    }
}
