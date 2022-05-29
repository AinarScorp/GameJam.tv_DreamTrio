using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyChasing))]
[RequireComponent(typeof(EnemyRetreat))]
[RequireComponent(typeof(EnemyWander))]
public class EnemyMeleeBehaviour : EnemyBehaviour
{
    [Header("Adjustments")]
    [SerializeField] [Range(0, 100)] float retreatChance = 10f;
    [Header("Melee Caching")]
    [SerializeField] Collider2D colider;
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

    public override void ReactToBeingHit(EnemyState stateToReactWith)
    {
        GetPushed();
        switch (CurrentState)
        {
            case EnemyState.Callibrating:
                SetNewEnemyState(EnemyState.Chasing);
                break;
            case EnemyState.Chasing:

                float randomRoll = Random.Range(0f, 100f);
                if (retreatChance >= randomRoll)
                    SetNewEnemyState(EnemyState.Retreating);
                break;

            default:
                return;
        }
        //if (CurrentState == EnemyState.Chasing)
        //{

        //    float randomRoll = Random.Range(0f, 100f);
        //    if (retreatChance >= randomRoll)
        //    {
        //        SetNewEnemyState(EnemyState.Retreating);
        //    }
        //    return;
        //}

    }

    void AdjustToNewState()
    {
        enemyChase.enabled = false;
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
            case EnemyState.Chasing:
                enemyChase.enabled = true;
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
