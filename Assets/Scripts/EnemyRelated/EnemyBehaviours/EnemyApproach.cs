using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyApproach : MonoBehaviour
{
    [Header("Adjustments")]
    [SerializeField] bool drawGizmos = true;

    [SerializeField] [Range(0, 200)] float approachSpeed = 80f;
    [SerializeField] float approachDistance = 80f;

    [Header("Caching")]

    [SerializeField] Rigidbody2D rb;
    [SerializeField] EnemyBehaviour behaviour;

    Coroutine checkPlayerPos;

    private void OnEnable()
    {
        checkPlayerPos = StartCoroutine(CheckPlayerPosition());
    }
    private void OnDisable()
    {
        if (checkPlayerPos != null)
            StopCoroutine(checkPlayerPos);
    }
    private void FixedUpdate()
    {

        Vector3 playerPos = behaviour.Player.transform.position;
        Vector3 direction = (playerPos - transform.position).normalized;
        rb.velocity = direction * approachSpeed * Time.fixedDeltaTime;
    }
    IEnumerator CheckPlayerPosition()
    {
        while (true)
        {
            if (this.enabled == false)
            {
                yield break;
            }
            CheckStopApproaching();
            yield return new WaitForSeconds(0.1f);
        }
    }
    void CheckStopApproaching()
    {

        float distanceFromPlayer = Vector3.Distance(transform.position, behaviour.Player.transform.position);
        if (distanceFromPlayer < approachDistance)
        {
            behaviour.SetNewEnemyState(EnemyState.Wandering);
            return;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos)
        {
            return;
        }
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, approachDistance);



    }
}
