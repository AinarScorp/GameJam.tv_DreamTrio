using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Adjustment")]
    [SerializeField] [Range(0, 500)] float speed = 10f;


    [Header("Better not touch")]
    [SerializeField] Rigidbody2D ghostRb;
    [SerializeField] PlayerGhostMovement ghost;


    Vector2 movementInputs;


    Rigidbody2D rb;
    PlayerInput input;

    public float Speed { get => speed; }

    private void Awake()
    {
        input = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        ghost.enabled = false;
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
        rb.velocity *= 0;
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

        rb.velocity = movementInputs * speed * Time.fixedDeltaTime;
    }
    void HandleRotation()
    {
        //Vector3 lookDirection = new Vector3(movementInputs.x, movementInputs.y).normalized;

        //if (lookDirection.magnitude <= 0.1f)
        //    return;
        //Quaternion targetDirection = Quaternion.LookRotation(lookDirection, Vector3.forward);
        //transform.rotation = targetDirection;
        if (movementInputs.magnitude <=0.1f)
        {
            return;

        }
        var angle = Mathf.Atan2(movementInputs.y, movementInputs.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    public void TurnIntoGhost()
    {

        ghost.transform.position = this.transform.position;
        ghost.enabled = true;
        this.enabled = false;
    }

}
