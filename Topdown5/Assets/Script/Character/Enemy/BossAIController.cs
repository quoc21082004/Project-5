using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BossAIController : MonoBehaviour
{
    private const float WAIT_UPDATE_INTERVAL = 1f;

    [Header("BasicReference")]
    [SerializeField] private Animator animator;
    [SerializeField] private BossAICombatHandler combatHandler;
    public Transform target;

    [Header("Prepare state field")]
    [SerializeField] private float detectRange;

    [field: SerializeField] public UnityEvent OnStartCombat { get; private set; }
    [field: SerializeField] public UnityEvent OnEndCombat { get; private set; }

    private void Awake()
    {
        combatHandler = GetComponent<BossAICombatHandler>();
    }
    private void Start()
    {
        StartCoroutine(BossFightCoroutine());
    }

    private IEnumerator BossFightCoroutine()
    {
        yield return WaitForPlayerInRange();
        yield return BeforeCombatState();
        yield return combatHandler.StartCombatState();
        yield return EndCombatState();
    }

    private IEnumerator WaitForPlayerInRange()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        while (true)
        {
            var distanceToPlayer = Vector2.Distance(transform.position, target.position);
            if (distanceToPlayer < detectRange)
            {
                yield break;
            }
            yield return new WaitForSeconds(WAIT_UPDATE_INTERVAL);
        }
    }
    private IEnumerator BeforeCombatState()
    {
        OnStartCombat.Invoke();
        yield break;
    }
    private IEnumerator EndCombatState()
    {
        OnEndCombat.Invoke();
        yield break;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}