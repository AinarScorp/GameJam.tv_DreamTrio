using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCallibration : MonoBehaviour
{
    [SerializeField] Vector3 destination;
    [SerializeField] float calibrationDuration;
    [SerializeField] [Range(0, 200)] float followSpeed = 30f;
    [SerializeField] EnemyState nextState;

    [Header("Caching")]

    [SerializeField] Rigidbody2D rb;
    [SerializeField] EnemyBehaviour behaviour;

    Coroutine timer;
    private void OnEnable()
    {

        timer = StartCoroutine(StartTimer());
    }
    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
        if (timer != null)
            StopCoroutine(timer);
    }
    private void FixedUpdate()
    {

        Vector3 direction = (destination - transform.position).normalized;
        rb.velocity = direction * followSpeed * Time.fixedDeltaTime;
        behaviour.Animator.SetFloat("Horizontal", Mathf.Clamp(rb.velocity.x, -1, 1));

    }
    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(calibrationDuration);
        behaviour.SetNewEnemyState(nextState);
    }
}
