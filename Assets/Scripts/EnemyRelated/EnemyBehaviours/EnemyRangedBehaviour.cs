using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyShooting))]
[RequireComponent(typeof(EnemyApproach))]
[RequireComponent(typeof(EnemyRetreat))]
[RequireComponent(typeof(EnemyWander))]
public class EnemyRangedBehaviour : EnemyBehaviour
{
    [Header("Adjustments")]
    [SerializeField] [Range(0, 100)] float retreatChance = 10f;
    [Header("Ranged Caching")]
    [SerializeField] Collider2D colider;
    [SerializeField] EnemyShooting enemyShooting;
    [SerializeField] EnemyApproach enemyApproach;
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
    public override void ReactToBeingHit(EnemyState stateToReactWith)
    {
        GetPushed();
        switch (CurrentState)
        {
            case EnemyState.Shooting:
                float randomRoll = Random.Range(0f, 100f);
                if (retreatChance >= randomRoll)
                    SetNewEnemyState(EnemyState.Retreating);
                break;

            case EnemyState.Approaching:
                SetNewEnemyState(EnemyState.Shooting);
                break;

            default:
                return;
        }

    }

    void AdjustToNewState()
    {
        enemyShooting.enabled = false;
        enemyApproach.enabled = false;
        enemyRetreat.enabled = false;
        enemyWander.enabled = false;
        colider.isTrigger = false;

        switch (CurrentState)
        {
            case EnemyState.Dying:
                break;
            case EnemyState.Callibrating:
                StartCallibration();
                break;
            case EnemyState.Shooting:
                enemyShooting.enabled = true;
                break;
            case EnemyState.Approaching:
                enemyApproach.enabled = true;
                break;
            case EnemyState.Retreating:
                colider.isTrigger = true;
                enemyRetreat.enabled = true;
                break;
            case EnemyState.Wandering:
                colider.isTrigger = true;
                enemyWander.enabled = true;
                break;
            default:
                return;
        }
    }
}
