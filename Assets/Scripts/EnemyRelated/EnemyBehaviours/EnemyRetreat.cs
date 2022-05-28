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
    //[Header("Checking for walls")]
    //[SerializeField] bool drawGizmos;
    //[SerializeField] float checkDistance = 1f;
    //[SerializeField] float checkRate = 0.4f;
    //[SerializeField] LayerMask defaultMask;


    [Header("Caching")]
    [SerializeField] EnemyBehaviour behaviour;
    [SerializeField] Rigidbody2D rb;

    Coroutine retreat;
    //Coroutine checkingForWalls;

    private void OnEnable()
    {
        retreat = StartCoroutine(StartRetreting());
    }
    private void OnDisable()
    {

        if (retreat != null)
            StopCoroutine(retreat);
        //if (checkingForWalls != null)
        //    StopCoroutine(checkingForWalls);

    }

    IEnumerator StartRetreting()
    {
        Vector3 direction = GetDireciton();

        //checkingForWalls = StartCoroutine(CheckingForWalls(direction));

        rb.velocity = direction * retreatSpeed * Time.fixedDeltaTime;
        behaviour.Animator.SetFloat("Horizontal", Mathf.Clamp(rb.velocity.x, -1, 1));
        yield return new WaitForSeconds(retreatDuration);
        FinishRetrating();
    }
    //IEnumerator CheckingForWalls(Vector3 direction)
    //{
    //    while (true)
    //    {
    //        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, checkDistance, defaultMask);
    //        if (hit.collider != null)
    //        {
    //            FinishRetrating();
    //        }
    //        yield return new WaitForSeconds(checkRate);
    //    }
    //}
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
    Vector3 GetDireciton()
    {
        Vector3 playerPos = behaviour.Player.transform.position;
        return (transform.position - playerPos).normalized;
        
    }

}
