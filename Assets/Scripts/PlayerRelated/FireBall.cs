using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D col;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] float explostionRadius = 10f;
    [SerializeField] int fireballDamage = 5;

    [SerializeField] float speed = 10f;
    [SerializeField] LayerMask colisionLayer;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] ParticleSystem explosion;
    bool exploded;

    Vector3 direction;

    private void FixedUpdate()
    {
        if (exploded)
        {
            return;
        }
        rb.velocity = direction * speed * Time.deltaTime *100;

    }
    public void FollowDirection(Vector3 direction)
    {
        this.direction = direction;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (exploded)
        {
            return;
        }
        DestroyMe();
        Explode();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explostionRadius, enemyLayer);
        foreach (var enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.ReceiveDamage(fireballDamage);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (exploded)
        {
            return;
        }
        DestroyMe();
        Explode();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explostionRadius, enemyLayer);
        foreach (var enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.ReceiveDamage(fireballDamage);
            }
        }
    }

    void Explode()
    {
        exploded = true;
        explosion.gameObject.SetActive(true);
        rb.velocity *= 0;

    }
    public void DestroyMe()
    {
        col.enabled = false;
        spriteRenderer.enabled = false;
        Destroy(this.gameObject, 3);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(this.transform.position, explostionRadius);
    }
}
