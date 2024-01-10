using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BossStormSpellCenter : MonoBehaviour, ISpell
{
    public float minRange;
    public float maxRange;
    public int Count;
    public GameObject stormprefab;
    public void KickOff(ActiveAbility ability, Vector2 direction)
    {
        Vector2 pos = (Vector2)transform.position;
        for (int i = 0; i < Count; i++)
        {
            if (Instantiate(stormprefab).TryGetComponent<BossStormSpell>(out BossStormSpell spell))
            {
                var dir = Random.Range(minRange, maxRange) * Random.insideUnitCircle;
                spell.KickOff(ability, pos + dir);
            }
        }
    }
}
