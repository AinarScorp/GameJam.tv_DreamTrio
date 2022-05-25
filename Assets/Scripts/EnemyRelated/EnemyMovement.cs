using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] float followSpeed = 30f;

    [SerializeField] Rigidbody2D rb;
    PlayerHealth player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerHealth>();
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
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

}
