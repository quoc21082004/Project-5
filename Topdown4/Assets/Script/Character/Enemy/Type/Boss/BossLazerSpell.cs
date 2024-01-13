using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLazerSpell : MonoBehaviour , ISpell
{
    private Collider2D mycollider;
    public float startSpellOffSet;
    public float duration;
    public float damageInterval;
    public float rotateLazer;
    [SerializeField] private BehaviourSpell behaviour = BehaviourSpell.RotateAround;
    private ActiveAbility activeAbility;
    Transform target;
    private void Awake()
    {
        behaviour = BehaviourSpell.RotateAround;
        mycollider = GetComponent<Collider2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    public void KickOff(ActiveAbility ability, Vector2 direction)
    {
        activeAbility = ability;
        Transform startPos = GameObject.Find("LazePosition").GetComponent<Transform>();
        if (startPos != null)
        {
            transform.position = startPos.position;
            transform.parent = startPos.transform;
        }
        transform.rotation = Quaternion.identity;
        RotateToDirection(direction);
        StartCoroutine(LazerFire());
    }
    private void RotateToDirection(Vector2 direction)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
    private IEnumerator LazerFire()
    {
        mycollider.enabled = false;
        yield return new WaitForSeconds(startSpellOffSet);
        RotateBehaviour();
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            mycollider.enabled = true;
            yield return new WaitForSeconds(damageInterval);
            mycollider.enabled = false;
        }
        Destroy(this.gameObject);
    }
    public void SetBehaviour(BehaviourSpell behaviour)
    {
        this.behaviour = behaviour;
    }
    private void RotateBehaviour()
    {
        switch(behaviour)
        {
            case BehaviourSpell.RotateAround:
                StartCoroutine(OneRoundRotate());
                break;
            case BehaviourSpell.ChasingTarget:
                StartCoroutine(ChasingPlayer());
                break;
            default:
                break;
        }
    }
    private IEnumerator OneRoundRotate()
    {
        while (true)
        {
            var targetAngle = transform.rotation.eulerAngles.z + rotateLazer;
            transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }
    private IEnumerator ChasingPlayer()
    {
        while (true)
        {
            Vector2 targetDirection = (Vector2)target.transform.position - (Vector2)transform.position;
            float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            targetAngle = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, targetAngle, rotateLazer);
            transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHurt>(out PlayerHurt target))
        {
            target.TakeDamage(null, activeAbility.skillInfo.baseDamage);
        }
    }
}
