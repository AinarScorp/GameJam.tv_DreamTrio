using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhostMovement : MonoBehaviour
{
    float speed = 10f;

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
        speed = FindObjectOfType<PlayerMovement>().Speed;
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
        gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }
    void HandleMovement()
    {

        rb.velocity = movementInputs * speed * Time.fixedDeltaTime;
    }
}
