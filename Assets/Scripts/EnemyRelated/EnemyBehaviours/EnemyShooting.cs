using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] bool drawGizmos = true;
    [SerializeField] float shootingRadius;
    [SerializeField] float shootingRate = 1f;

    [SerializeField] EnemyBehaviour behaviour;
    [SerializeField] Transform shootingBase;
    [SerializeField] Projectile projectile;
    [SerializeField] Rigidbody2D rb;



    Coroutine shooting;


    private void OnEnable()
    {
        shooting = StartCoroutine(StartShooting());
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    private void OnDisable()
    {
        if (shooting != null)
            StopCoroutine(shooting);
        rb.constraints = ~RigidbodyConstraints2D.FreezePosition;

    }


    IEnumerator StartShooting()
    {

        while (true)
        {
            Vector3 playerPos = behaviour.Player.transform.position;
            float distance = Vector3.Distance(transform.position, playerPos);

            if (distance> shootingRadius)
            {
                behaviour.SetNewEnemyState(EnemyState.Wandering);
                yield break;
            }


            Vector3 projectileDirection = (playerPos - shootingBase.position).normalized;
            behaviour.Animator.SetFloat("Horizontal", Mathf.Clamp(projectileDirection.x, -1, 1));

            Projectile bullet = Instantiate(projectile, shootingBase.position, Quaternion.identity, shootingBase);
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
