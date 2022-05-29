using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] bool drawGizmos = true;
    [SerializeField] float shootingRadius;
    [SerializeField] float shootingRate = 1f;
    [SerializeField] float flyingDelay = 1f;


    [SerializeField] EnemyBehaviour behaviour;
    [SerializeField] Transform shootingBase;
    [SerializeField] Projectile projectile;
    [SerializeField] Rigidbody2D rb;


    Vector3 playerPos;
    Coroutine shooting;
    Coroutine animationAdjustment;
    Projectile bullet;

    private void OnEnable()
    {
        shooting = StartCoroutine(StartShooting());
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    private void OnDisable()
    {

        StopAllCoroutines();
        rb.constraints = ~RigidbodyConstraints2D.FreezePosition;
        if (bullet != null)
        {
            bullet.DestroyMe();
        }
    }


    IEnumerator AdjustAnimation()
    {
        while (true)
        {
            playerPos = behaviour.Player.transform.position;

            Vector3 projectileDirection = (playerPos - shootingBase.position).normalized;
            behaviour.Animator.SetFloat("Horizontal", Mathf.Clamp(projectileDirection.x, -1, 1));

            yield return new WaitForSeconds(0.1f);

        }
    }
    IEnumerator StartShooting()
    {
        animationAdjustment = StartCoroutine(AdjustAnimation());
        while (true)
        {
            float distance = Vector3.Distance(transform.position, playerPos);

            if (distance> shootingRadius)
            {
                behaviour.SetNewEnemyState(EnemyState.Wandering);
                yield break;
            }



            bullet = Instantiate(projectile, shootingBase.position, Quaternion.identity, shootingBase);
            yield return new WaitForSeconds(flyingDelay);
            Vector3 projectileDirection = (playerPos - shootingBase.position).normalized;
            bullet.FollowDirection(projectileDirection);
            
            yield return new WaitForSeconds(shootingRate);

        }
    }
    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos)
            return;

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(shootingBase.position, shootingRadius);
    }
}
