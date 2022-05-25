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

        Vector3 direction = ( player.transform.position - transform.position).normalized;
        rb.velocity = direction * followSpeed * Time.fixedDeltaTime;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.ReceiveDamage();

            //Rigidbody2D playerRigidBody = player.GetComponent<Rigidbody2D>();

            //player.enabled = false;
            //playerRigidBody.AddForce(direction * pushForce * Time.deltaTime, ForceMode2D.Force);
            if (playerHealth.CurrentHealth > 0)
            {
                Vector3 direction = (player.transform.position - transform.position).normalized;
                player.GetComponent<PlayerMovement>().PushPlayer(direction);
            }
        }
    }

}
