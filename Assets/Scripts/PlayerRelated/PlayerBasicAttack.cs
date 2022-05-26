using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttack : MonoBehaviour
{
    public static PlayerBasicAttack Instance { get; private set; }

    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] SpriteRenderer spriteRenderer;


    [HideInInspector] [SerializeField] bool drawPositionLines;
    [HideInInspector] [SerializeField] [Range(0.1f, 2f)] float attackPointDistance = 0.5f;

    [HideInInspector] [SerializeField] bool drawRadiusCircle;
    [HideInInspector] [SerializeField] bool drawAllCircles;

    [HideInInspector] [SerializeField] [Range(0, 1.5f)] float attackRadius = 1f;



    bool isAttacking;

    PlayerInput input;
    PlayerMovement playerMovement;
    Animator animator;


    public bool IsAttacking { get => isAttacking; }


    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;

        input = new PlayerInput();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        if (spriteRenderer == null)
            spriteRenderer = GameObject.FindGameObjectWithTag("Slash Attack").GetComponent<SpriteRenderer>();

    }

    private void OnEnable() => input.Enable();

    private void OnDisable()
    {
        FinishAttack();
        input.Disable();
    }

    private void Start()
    {
        spriteRenderer.enabled = false;

        input.PlayerBasic.MeleeAttack.performed += _ => Attack();

        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        playerManager.SubscribeToImmidiateActions(() => this.enabled = false, true);
        playerManager.SubscribeToActivateControls(() => this.enabled = true, false);

    }




    void RotateAttackBase()
    {
        attackPoint.localPosition = playerMovement.FacingDirection * attackPointDistance;

    }
    void Attack()
    {
        if (!IsAttacking)
        {
            isAttacking = true;
            animator.SetBool("CanMove", false);
            if (!animator.GetBool("FirstLightAttackPlaying"))
            {
                animator.SetTrigger("Attack");
                animator.SetBool("FirstLightAttackPlaying", true);
            }

        }
    }
    public void FinishAttack() => isAttacking = false;

    void ContactAttack()
    {
        RotateAttackBase();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
        foreach (var enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.ReceiveDamage();
            }
        }
    }




    #region editor
    public void SetDrawPositionLines(bool setTo) => drawPositionLines = setTo;
    public void SetDrawRadiusCircle(bool setTo) => drawRadiusCircle = setTo;
    public void SetDrawAllCircles(bool setTo) => drawAllCircles = setTo;
    #endregion

    #region comments /useless stuff


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

    //             was part of void RotateAttackBase()
    //var angle = Mathf.Atan2(playerMovement.FacingDirection.y, playerMovement.FacingDirection.x) * Mathf.Rad2Deg;

    //spriteRenderer.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //spriteRenderer.transform.localPosition = playerMovement.FacingDirection * 0.5f;
    #endregion
}
