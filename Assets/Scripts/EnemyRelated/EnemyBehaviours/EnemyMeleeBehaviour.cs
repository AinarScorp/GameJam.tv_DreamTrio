using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyChasing))]
[RequireComponent(typeof(EnemyRetreat))]
[RequireComponent(typeof(EnemyWander))]
public class EnemyMeleeBehaviour : EnemyBehaviour
{
    [Header("Adjustments")]
    [SerializeField] float pushForce = 1f;
    [SerializeField] [Range(0, 100)] float retreatChance = 10f;
    [Header("Melee Caching")]

    [SerializeField] EnemyChasing enemyChase;
    [SerializeField] EnemyRetreat enemyRetreat;
    [SerializeField] EnemyWander enemyWander;


    public override void Awake()
    {
        base.Awake();
        FindObjectOfType<PlayerManager>().SubscribeToImmidiateActions(() => SetNewEnemyState(EnemyState.Retreating), true);
        GetComponent<EnemyHealth>().SubscribeToReactHit(() => ReactToBeingHit(EnemyState.Chasing));
    }


    public override void SetNewEnemyState(EnemyState newState)
    {
        base.SetNewEnemyState(newState);
        AdjustToNewState();

    }
    void GetPushed()
    {
        Vector3 pushDirection = (transform.position - Player.transform.position).normalized;
        Vector3 newPlayerPos = transform.position + pushDirection * pushForce;

        transform.position = newPlayerPos;
    }
    public override void ReactToBeingHit(EnemyState stateToReactWith)
    {
        GetPushed();
        if (CurrentState == EnemyState.Chasing)
        {
            float randomRoll = Random.Range(0f, 100f);
            if (retreatChance >= randomRoll)
                SetNewEnemyState(EnemyState.Retreating);
            return;
        }
        base.ReactToBeingHit(stateToReactWith);

    }

    void AdjustToNewState()
    {
        enemyChase.enabled = false;
        enemyRetreat.enabled = false;
        enemyWander.enabled = false;

        switch (CurrentState)
        {
            case EnemyState.Dying:
                return;
            case EnemyState.Chasing:
                enemyChase.enabled = true;
                break;
            case EnemyState.Retreating:
                enemyRetreat.enabled = true;
                break;
            case EnemyState.Wandering:
                enemyWander.enabled = true;
                break;
            default:
                return;
        }
    }
}
