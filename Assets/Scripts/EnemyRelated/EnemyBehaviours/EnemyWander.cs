using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWander : MonoBehaviour
{
    #region adjustments
    [Header("Adjustments")]
    [SerializeField] bool drawGizmos = true;
    [SerializeField] float playerTooFar = 10f;
    [SerializeField] float playerTooClose = 2f;
    [SerializeField] float forcedWanderDuration = 2f;
    [SerializeField] float wanderSpeed = 30f;
    [SerializeField] float wanderDistance = 2f;
    #endregion

    [Header("Caching")]
    [SerializeField] EnemyBehaviour behaviour;
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] Rigidbody2D rb;

    Vector3 destination;
    bool forcedWander;
    Coroutine forcedWanderCorotine;
    Coroutine checkPlayerPos;

    private void OnEnable()
    {
        forcedWanderCorotine = StartCoroutine(SetupForcedWander());
        checkPlayerPos = StartCoroutine(CheckPlayerPosition());

        StartMovingToNewPosition();

    }
    private void OnDisable()
    {
        StopCoroutine(forcedWanderCorotine);
        StopCoroutine(checkPlayerPos);

    }


    private void Start()
    {
        enemyHealth.SubscribeToReactHit(() => SwitchForcedWander(false));
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, destination) > 0.1f)
        {
            return;
        }
        StartMovingToNewPosition();
    }
    IEnumerator SetupForcedWander()
    {
        forcedWander = true;
        yield return new WaitForSeconds(forcedWanderDuration);
        forcedWander = false;

    }

    IEnumerator CheckPlayerPosition()
    {
        while (true)
        {
            StopWanderIfTooFar();
            StopWanderIfTooClose();
            yield return new WaitForSeconds(1f);
        }
    }
    private void StartMovingToNewPosition()
    {
        Vector3 direction = ChooseRandomDirection();
        destination = transform.position + direction * wanderDistance;
        rb.velocity = direction * wanderSpeed * Time.fixedDeltaTime;
    }
    private void StopWanderIfTooClose()
    {
        if (!behaviour.Player.IsAlive || forcedWander)
            return;
        float distanceFromPlayer = Vector3.Distance(transform.position, behaviour.Player.transform.position);
        if (distanceFromPlayer < playerTooFar)
        {
            behaviour.SetNewEnemyState(EnemyState.Chasing);
        }
    }
    private void StopWanderIfTooFar()
    {
        if (!behaviour.Player.IsAlive || forcedWander)
            return;
        float distanceFromPlayer = Vector3.Distance(transform.position, behaviour.Player.transform.position);
        if (distanceFromPlayer > playerTooFar)
        {
            behaviour.SetNewEnemyState(EnemyState.Chasing);
        }
    }

    Vector3 ChooseRandomDirection()
    {
        float randX = Random.Range(-1f, 1f);
        float randY = Random.Range(-1f, 1f);
        return new Vector3(randX, randY).normalized;
    }
    
    void SwitchForcedWander(bool switchTo) => forcedWander = switchTo;
    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerTooFar);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerTooClose);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, wanderDistance);
        if (destination.sqrMagnitude >0f)
        {

        Gizmos.DrawLine(transform.position, destination);
        }


    }
}
