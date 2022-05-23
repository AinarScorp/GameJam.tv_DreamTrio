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
    PlayerMovement playerMovement;


    Animator animator;

    //Vector2 movementInputs;
    //Vector2 facingDirection;
    private void Awake()
    {
        input = new PlayerInput();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable() => input.Enable();

    private void OnDisable() => input.Disable();

    private void Start()
    {
        spriteRenderer.enabled = false;

        input.PlayerBasic.MeleeAttack.performed += _ => Attack();


        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        playerHealth.SubscribeToPlayerDeathPermanently(() => this.enabled = false);
        playerHealth.SubscribeToRevival(() => this.enabled = true);
    }


    //void RecogniseFacingDirection()
    //{

    //    if (movementInputs.x != 0)
    //    {
    //        facingDirection.x = Mathf.Round(movementInputs.x);
    //        facingDirection.y = 0;

    //    }
    //    else if (movementInputs.y != 0)
    //    {
    //        facingDirection.y = Mathf.Round(movementInputs.y);
    //        facingDirection.x = 0;

    //    }
    //}

    void RotateAttackBase()
    {
        attackPoint.localPosition = playerMovement.FacingDirection * 0.5f;

        //var angle = Mathf.Atan2(playerMovement.FacingDirection.y, playerMovement.FacingDirection.x) * Mathf.Rad2Deg;

        //spriteRenderer.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //spriteRenderer.transform.localPosition = playerMovement.FacingDirection * 0.5f;
    }
    void Attack()
    {
        RotateAttackBase();
        //spriteRenderer.sprite = slashSprite;
        animator.SetTrigger("FirstLightAttack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
        foreach (var enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.ReceiveDamage();
            }
        }
        StartCoroutine(ResetSlash());
    }
    IEnumerator ResetSlash()
    {
        yield return new WaitForSeconds(secondsBeforeTurningOffSlash);
        //spriteRenderer.sprite = null;
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
