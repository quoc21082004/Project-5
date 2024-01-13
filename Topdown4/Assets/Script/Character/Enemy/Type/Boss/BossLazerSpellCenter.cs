using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLazerSpellCenter : MonoBehaviour, ISpell
{
    [Range(1, 10)] public int Count;
    public GameObject lazerprefab;
    Transform lazePos;
    private void Awake()
    {
        lazePos = GameObject.Find("LazePosition").GetComponent<Transform>(); 
    }
    public void KickOff(ActiveAbility ability, Vector2 direction)
    {
        transform.parent = lazePos;
        List<Vector2> directions = CaculateLazerAngle(direction, Count, 360);
        for (int i = 0; i < Count; i++)
        {
            if (Instantiate(lazerprefab).gameObject.TryGetComponent<BossLazerSpell>(out BossLazerSpell spell))
            {
                spell.SetBehaviour(BehaviourSpell.RotateAround);
                spell.KickOff(ability, directions[i]);
            }
        }
    }
    public List<Vector2> CaculateLazerAngle(Vector2 direction, int count , float angle)
    {
        float fireAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float differentAngle = angle / count;
        var result = new List<Vector2>();
        for (int i = 0; i < count; i++)
        {
            fireAngle = (fireAngle + differentAngle) % 360;
            float fireRad = fireAngle * Mathf.Deg2Rad;
            Vector2 fireDirect = new(Mathf.Cos(fireRad), Mathf.Sin(fireRad));
            result.Add(fireDirect);
        }
        return result;
    }
}
