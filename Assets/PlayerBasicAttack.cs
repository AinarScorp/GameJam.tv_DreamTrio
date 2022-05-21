using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttack : MonoBehaviour
{
    [Header("Hit enemies Settings")]
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRadius = 2f;
    [SerializeField] LayerMask enemyLayer;

    [Header("Slash Setttings")]
    [SerializeField] Sprite slashSprite;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float secondsBeforeTurningOffSlash = 0.1f;

    PlayerInput input;
    private void Awake()
    {
        input = new PlayerInput();
    }

    private void OnEnable() => input.Enable();

    private void OnDisable() => input.Disable();

    private void Start()
    {
        spriteRenderer.sprite = null;

        input.PlayerBasic.MeleeAttack.performed += _ => Attack();

    }

    void Attack()
    {
        spriteRenderer.sprite = slashSprite;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
        foreach (var enemy in hitEnemies)
        {
            Debug.Log("you hit " + enemy.name);
        }
        StartCoroutine(ResetSlash());
    }
    IEnumerator ResetSlash()
    {
        yield return new WaitForSeconds(secondsBeforeTurningOffSlash);
        spriteRenderer.sprite = null;
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
