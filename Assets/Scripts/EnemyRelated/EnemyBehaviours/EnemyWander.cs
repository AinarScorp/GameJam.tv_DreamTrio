using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWander : MonoBehaviour
{
    #region adjustments
    [Header("Adjustments")]

    [SerializeField] bool drawGizmos = true;
    [SerializeField] EnemyState nextStateIfClose;
    [SerializeField] EnemyState nextStateIfFar;
    [SerializeField] float playerTooFar = 10f;
    [SerializeField] float playerTooClose = 2f;
    [SerializeField] float wanderSpeed = 30f;
    [SerializeField] float wanderDistance = 2f;

    #endregion

    [Header("Caching")]
    [SerializeField] EnemyBehaviour behaviour;
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] Rigidbody2D rb;

    Vector3 destination;
    Coroutine checkPlayerPos;

    private void OnEnable()
    {
        checkPlayerPos = StartCoroutine(CheckPlayerPosition());

        StartMovingToNewPosition();

    }
    private void OnDisable()
    {
        if (checkPlayerPos !=null)
            StopCoroutine(checkPlayerPos);
    }




    private void FixedUpdate()
    {
        behaviour.Animator.SetFloat("Horizontal", Mathf.Clamp(rb.velocity.x, -1, 1));
        if (Vector3.Distance(transform.position, destination) > 0.1f)
        {
            return;
        }
        StartMovingToNewPosition();

    }


    IEnumerator CheckPlayerPosition()
    {
        while (true)
        {
            if (this.enabled == false)
            {
                yield break;
            }
            CheckStopWander();
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void StartMovingToNewPosition()
    {
        Vector3 direction = ChooseRandomDirection();
        destination = transform.position + direction * wanderDistance;
        rb.velocity = direction * wanderSpeed * Time.fixedDeltaTime;
    }

    void CheckStopWander()
    {
        if (!behaviour.Player.IsAlive)
            return;
        float distanceFromPlayer = Vector3.Distance(transform.position, behaviour.Player.transform.position);
        if (distanceFromPlayer < playerTooClose)
        {
            behaviour.SetNewEnemyState(nextStateIfClose);
            return;
        }
        if (distanceFromPlayer > playerTooFar)
        {

            behaviour.SetNewEnemyState(nextStateIfFar);
            return;
        }
    }
    //private void StopWanderIfTooClose()
    //{
    //    if (!behaviour.Player.IsAlive)
    //        return;
    //    float distanceFromPlayer = Vector3.Distance(transform.position, behaviour.Player.transform.position);
    //    if (distanceFromPlayer < playerTooClose)
    //    {
    //        behaviour.SetNewEnemyState(EnemyState.Chasing);
    //    }
    //}
    //private void StopWanderIfTooFar()
    //{
    //    if (!behaviour.Player.IsAlive)
    //        return;
    //    float distanceFromPlayer = Vector3.Distance(transform.position, behaviour.Player.transform.position);
    //    if (distanceFromPlayer > playerTooFar)
    //    {
    //        behaviour.SetNewEnemyState(EnemyState.Chasing);
    //    }
    //}

    Vector3 ChooseRandomDirection()
    {
        float randX = Random.Range(-1f, 1f);
        float randY = Random.Range(-1f, 1f);
        return new Vector3(randX, randY).normalized;
    }
    #region forced wander - disabled

    //[SerializeField] float forcedWanderDuration = 2f;

    //bool forcedWander;
    //Coroutine forcedWanderCorotine;

    //OnEnable
    //forcedWanderCorotine = StartCoroutine(SetupForcedWander());

    //OnDisbale 
    //if (forcedWanderCorotine != null)
    //    StopCoroutine(forcedWanderCorotine);

    //Start function
    //enemyHealth.SubscribeToReactHit(() => SwitchForcedWander(false));



    //IEnumerator SetupForcedWander()
    //{
    //    forcedWander = true;
    //    yield return new WaitForSeconds(forcedWanderDuration);
    //    forcedWander = false;

    //}
    //void SwitchForcedWander(bool switchTo) => forcedWander = switchTo;

    #endregion
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
