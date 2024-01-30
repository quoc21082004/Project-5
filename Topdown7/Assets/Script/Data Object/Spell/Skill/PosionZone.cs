using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosionZone : MonoBehaviour, ISpell
{
    Animator myanim;
    private ActiveAbility abilitySpell;
    Collider2D[] colliders;
    public LayerMask layerMask;
    public float prepareTime;
    public float durationTime;
    public float damageTimeEclipse;
    public float damageRange;
    private void Awake()
    {
        myanim = GetComponent<Animator>();
    }
    public void KickOff(ActiveAbility ability, Vector2 direction)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos) ;
        abilitySpell = ability;
        direction = Vector2.ClampMagnitude(direction, abilitySpell.MaxUseRange);
        transform.position = mousePos - (Vector3)direction;
        StartCoroutine(StartPrepare());
    }
    private float CaculateDamage()
    {
        float ratePercent = Random.Range(150f, 170f);
        float totalDamage = (((abilitySpell.skillInfo.baseDamage + PlayerPrefs.GetFloat("wandDamage")) * ratePercent) / 100) * Random.Range(1.75f, 2.5f);
        return totalDamage;
    }
    private IEnumerator StartPrepare()
    {
        yield return new WaitForSeconds(prepareTime);
        myanim.Play("PoionZoneDamage");
        var endTime = Time.time + durationTime;
        while (Time.time <= endTime) 
        {
            colliders = Physics2D.OverlapCircleAll(transform.position, damageRange, layerMask);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent<EnemyHurt>(out EnemyHurt enemy))
                    enemy.TakeDamage(CaculateDamage(), false);
            }
            yield return new WaitForSeconds(damageTimeEclipse);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(transform.position, damageRange);
    }
}
