using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] float followSpeed = 30f;
    [SerializeField] [Range(0.1f, 5f)] float secondsWalkingAway = 1f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    PlayerHealth player;
    bool movingAway;

    private void Awake()
    {
        player = FindObjectOfType<PlayerHealth>();
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }
    private void OnDisable()
    {
        animator.SetFloat("Horizontal", Mathf.Clamp(rb.velocity.x, -1, 1));
        rb.velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
        FindObjectOfType<PlayerManager>()?.UnsubscribeFromImmidiateActions(LeaveDeadPlayer, true);

    }
    private void Start()
    {
        FindObjectOfType<PlayerManager>().SubscribeToImmidiateActions(LeaveDeadPlayer, true);
    }
    private void Update()
    {
        animator.SetFloat("Horizontal", Mathf.Clamp(rb.velocity.x, -1, 1));
    }
    private void FixedUpdate()
    {
        if (movingAway)
            return;

        if (!player.IsAlive)
        {
            rb.velocity = Vector2.zero;

            return;
        }
        Vector3 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * followSpeed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!player.IsAlive || player.IsInvincible)
            return;
        if (collision.gameObject == player.gameObject)
        {
            player.ReceiveDamage();
            if (player.CurrentHealth > 0)
            {

                Vector3 direction = (player.transform.position - transform.position).normalized;
                player.GetComponent<PlayerMovement>().PushPlayer(direction);
            }


        }
    }
    void LeaveDeadPlayer()
    {
        StartCoroutine(GivePlayerSpace());
    }
    IEnumerator GivePlayerSpace()
    {
        movingAway = true;
        Vector3 direction = (transform.position - player.transform.position).normalized;
        rb.velocity = direction * followSpeed * Time.fixedDeltaTime;
        yield return new WaitForSeconds(secondsWalkingAway);
        movingAway = false;
    }
}
