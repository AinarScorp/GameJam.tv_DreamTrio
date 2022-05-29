using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 10f;
    [SerializeField] float lifespan = 5f;
    [SerializeField] LayerMask colisionLayer;
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] SpriteRenderer spriteRenderer;

    bool moving = false;
    Vector3 direction;

    private void FixedUpdate()
    {
        if (!moving)
            return;

        rb.velocity = direction * speed * Time.deltaTime;

    }
    public void FollowDirection(Vector3 direction)
    {
        moving = true;
        this.direction = direction;
        StartCoroutine(DestroyAfterLifespan());
    }
    IEnumerator DestroyAfterLifespan(float customLifespan = 0)
    {
        if (customLifespan != 0)
            lifespan = customLifespan;

        yield return new WaitForSeconds(lifespan);
        DestroyMe();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth == null || !playerHealth.IsAlive)
            return;

        moving = false;
        rb.velocity *= 0;

        explosionParticle.gameObject.SetActive(true);
        if (playerHealth != null)
            playerHealth.ReceiveDamage();
        
        DestroyMe();
    }

    void DestroyMe()
    {
        
        spriteRenderer.enabled = false;
        Destroy(this.gameObject,3);
    }
}
