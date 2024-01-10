using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpellCenter : MonoBehaviour, ISpell
{
    [Range(1, 10)] public float minRange;
    [Range(1, 10)] public float maxRange;
    public Transform[] spikePos;
    public GameObject spikeprefab;
    private ActiveAbility activeAbility;
    
    public void KickOff(ActiveAbility ability, Vector2 direction)
    {
        activeAbility = ability;
        foreach (var spawonPoint in spikePos)
        {
            if (Instantiate(spikeprefab).TryGetComponent<SpikeSpell>(out SpikeSpell spike))
            {
                spike.transform.parent = transform;
                spike.KickOff(ability, spawonPoint.position);
            }
        }
    }
}
