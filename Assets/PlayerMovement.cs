using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Adjustment")]
    [SerializeField] [Range(0, 500)] float speed = 10f;
    [SerializeField] [Range(0, 500)] float radius = 10f;
    [SerializeField] bool isDead;
    [SerializeField] Rigidbody2D ghostRb;

    Vector2 movementInputs;


    Rigidbody2D rb;
    PlayerInput input;


    private void Awake()
    {
        input = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }
    private void Start()
    {
        input.PlayerBasic.Movement.performed += ctx => movementInputs = ctx.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }
    void HandleMovement()
    {
        if (isDead)
        {
            ghostRb.velocity = movementInputs * speed * Time.fixedDeltaTime;
            return;
        }
        rb.velocity = movementInputs * speed * Time.fixedDeltaTime;
    }
    void HandleRotation()
    {
        //Vector3 lookDirection = new Vector3(movementInputs.x, movementInputs.y).normalized;

        //if (lookDirection.magnitude <= 0.1f)
        //    return;
        //Quaternion targetDirection = Quaternion.LookRotation(lookDirection, Vector3.forward);
        //transform.rotation = targetDirection;
        if (isDead)
        {
            return;
        }
        if (movementInputs.magnitude <=0.1f)
        {
            return;

        }
        var angle = Mathf.Atan2(movementInputs.y, movementInputs.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
