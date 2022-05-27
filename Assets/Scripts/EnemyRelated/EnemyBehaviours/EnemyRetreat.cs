using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRetreat : MonoBehaviour
{
    //[Header("states after retreating depending on the Player State")]
    //[Tooltip("this state will activate if the player is Alive")]
    //[SerializeField] EnemyState stateIfAlive;
    //[Tooltip("this state will activate if the player is Dead")]
    //[SerializeField] EnemyState stateIfDead;

    [SerializeField] float retreatSpeed = 20f;

    [SerializeField] float retreatDuration = 2f;

    [Header("Caching")]
    [SerializeField] EnemyBehaviour behaviour;
    [SerializeField] Rigidbody2D rb;


    Coroutine retreat;

    private void OnEnable()
    {
        retreat = StartCoroutine(StartRetreting());
    }
    private void OnDisable()
    {

        if (retreat != null)
            StopCoroutine(retreat);

    }

    IEnumerator StartRetreting()
    {
        Vector3 playerPos = behaviour.Player.transform.position;
        Vector3 direction = (transform.position - playerPos).normalized;
        rb.velocity = direction * retreatSpeed * Time.fixedDeltaTime;
        yield return new WaitForSeconds(retreatDuration);
        FinishRetrating();
    }
    void FinishRetrating()
    {
        //if (behaviour.Player.IsAlive)
        //{
        //    behaviour.SetNewEnemyState(stateIfAlive);
        //}
        //else
        //{
        //    behaviour.SetNewEnemyState(stateIfDead);
        //}
        behaviour.SetNewEnemyState(EnemyState.Wandering);

    }
}
