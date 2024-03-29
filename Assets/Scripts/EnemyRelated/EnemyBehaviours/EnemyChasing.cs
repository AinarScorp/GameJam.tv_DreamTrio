using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChasing : MonoBehaviour
{

    [Header("Adjustments")]

    [SerializeField] [Range(0, 200)] float followSpeed = 30f;
    [SerializeField] [Range(0, 100)] float minChaseDuration = 30f;

    [SerializeField] [Range(0, 100)] float maxChaseDuration = 60f;
    [SerializeField] [Range(0, 100f)] float hitAndRunChance = 10f;
    [Header("Caching")]

    [SerializeField] Rigidbody2D rb;
    [SerializeField] EnemyBehaviour behaviour;


    Coroutine timer;
    private void Awake()
    {
        if (behaviour == null)
        {
            behaviour = GetComponent<EnemyMeleeBehaviour>();
        }
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }
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

        Vector3 playerPos = behaviour.Player.transform.position;
        Vector3 direction = (playerPos - transform.position).normalized;
        rb.velocity = direction * followSpeed * Time.fixedDeltaTime;
        behaviour.Animator.SetFloat("Horizontal", Mathf.Clamp(rb.velocity.x, -1, 1));

    }
    IEnumerator StartTimer()
    {
        float duration = Random.Range(minChaseDuration, maxChaseDuration);
        yield return new WaitForSeconds(duration);
        behaviour.SetNewEnemyState(EnemyState.Retreating);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!behaviour.Player.IsAlive || behaviour.Player.IsInvincible || this.enabled == false)
        {

            behaviour.SetNewEnemyState(EnemyState.Retreating);
            return;
        }
        if (collision.gameObject == behaviour.Player.gameObject)
        {
            behaviour.Player.ReceiveDamage();
            if (behaviour.Player.CurrentHealth > 0)
            {
                Vector3 playerPos = behaviour.Player.transform.position;

                Vector3 direction = (playerPos - transform.position).normalized;
                PlayerMovement playerMovement = behaviour.Player.GetComponent<PlayerMovement>();
                playerMovement.PushPlayer(direction);
                float randomRoll = Random.Range(0f, 100f);
                if (hitAndRunChance >= randomRoll)
                    behaviour.SetNewEnemyState(EnemyState.Retreating);
            }


        }
    }

}
