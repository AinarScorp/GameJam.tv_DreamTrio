using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhostMovement : MonoBehaviour
{
    [SerializeField] Animator animator;

    bool animationIsPlaying;
    float speed = 10f;

    Vector2 movementInputs;

    Rigidbody2D rb;
    PlayerInput input;

    public bool AnimationIsPlaying { get => animationIsPlaying; }
    private void Awake()
    {
        input = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
        speed = FindObjectOfType<PlayerMovement>().Speed;

        if (animator == null)
            animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        animationIsPlaying = true;
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
        input.PlayerBasic.Movement.canceled += _ => movementInputs *= 0;
        //PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        //playerManager.SubscribeToActivateControls(() => this.enabled = true, true);
        gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (animationIsPlaying)
        {
            return;
        }
        HandleMovement();
    }
    void HandleMovement()
    {
        if (movementInputs.sqrMagnitude <0)
            rb.velocity *= 0;

        animator.SetFloat("Horizontal", movementInputs.x);
        animator.SetFloat("Vertical", movementInputs.y);
        rb.velocity = movementInputs * speed * Time.fixedDeltaTime;
    }
    public void GhostVanish()
    {
        animationIsPlaying = true;
        animator.SetTrigger("Died");
        StartCoroutine(TurnOffGameObject());
    }
    IEnumerator TurnOffGameObject()
    {
        while (animationIsPlaying)
        {
            yield return null;

        }
        gameObject.SetActive(false);

    }

    void StopAnimationPlaying() => animationIsPlaying = false;

}
