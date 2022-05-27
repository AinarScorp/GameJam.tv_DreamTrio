using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Adjustment")]
    [SerializeField] [Range(0, 500)] float speed = 10f;
    [SerializeField] [Range(0, 20)] float pushForce = 10f;
    [SerializeField] LayerMask defaultMask;


    [Header("Better not touch")]

    bool canMove;
    Vector2 movementInputs;
    Vector2 facingDirection;

    Animator animator;
    Rigidbody2D rb;
    PlayerInput input;

    public float Speed { get => speed; }
    public Vector2 FacingDirection { get => facingDirection; }
    public Vector2 MovementInputs { get => movementInputs; }

    private void Awake()
    {
        input = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
        rb.velocity *= 0;
    }
    private void Start()
    {
        facingDirection = Vector2.right;
        input.PlayerBasic.Movement.performed += ctx => movementInputs = ctx.ReadValue<Vector2>();
        input.PlayerBasic.Movement.canceled += _ => movementInputs *= 0;

        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        playerManager.SubscribeToImmidiateActions(() => this.enabled = false, true);
        playerManager.SubscribeToActivateControls(() => this.enabled = true, false);

    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }
    void HandleMovement()
    {
        if (!animator.GetBool("CanMove") || movementInputs.sqrMagnitude <0)
        {
            rb.velocity *= 0;

            return;
        }
        animator.SetFloat("Horizontal", movementInputs.x);
        animator.SetFloat("Vertical", movementInputs.y);
        animator.SetFloat("Speed", movementInputs.sqrMagnitude);
        rb.velocity = movementInputs * speed * Time.fixedDeltaTime;
    }
    void HandleRotation()
    {
        //method 1
        //Vector3 lookDirection = new Vector3(movementInputs.x, movementInputs.y).normalized;

        //if (lookDirection.magnitude <= 0.1f)
        //    return;
        //Quaternion targetDirection = Quaternion.LookRotation(lookDirection, Vector3.forward);
        //transform.rotation = targetDirection;

        //method 2

        //if (movementInputs.magnitude <=0.1f)
        //{
        //    return;

        //}
        //var angle = Mathf.Atan2(movementInputs.y, movementInputs.x) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        //method 3

        //if (movementInputs.x != 0 )
        //{
        //    Mathf.Round(movementInputs.x);
        //    Vector3 newScale = transform.localScale;
        //    newScale.x *= -1;
        //    transform.localScale = newScale;

        //}

        if (movementInputs.x != 0)
        {
            facingDirection.x = Mathf.Round(movementInputs.x);
            facingDirection.y = 0;

        }
        else if (movementInputs.y != 0)
        {
            facingDirection.y = Mathf.Round(movementInputs.y);
            facingDirection.x = 0;

        }
        animator.SetFloat("FacingRight", facingDirection.x);
        animator.SetFloat("FacingUp", facingDirection.y);

    }

    public void SetCanMove(bool setTo) => canMove = setTo;
    public void PushPlayer(Vector3 pushDirection)
    {
        Vector3 newPlayerPos = transform.position + pushDirection * pushForce;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, pushDirection, pushForce, defaultMask);
        if (hit.collider !=null)
        {
            return;
        }
        transform.position = newPlayerPos;
    }
}
