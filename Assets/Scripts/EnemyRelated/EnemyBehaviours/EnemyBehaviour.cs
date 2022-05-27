using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Basic Behaviour Settings")]
    [SerializeField] float pushForce = 0.5f;

    [SerializeField] LayerMask defaultMask;

    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] EnemyState startingState;
    [SerializeField] EnemyState borderState;


    EnemyState currentState;
    PlayerHealth player;
    public EnemyState CurrentState { get => currentState; }
    public PlayerHealth Player { get => player; }
    public EnemyState BorderState { get => borderState;  }
    public float PushForce { get => pushForce; }

    public virtual void Awake()
    {
        player = FindObjectOfType<PlayerHealth>();

    }

    private void Start()
    {
        SetNewEnemyState(startingState);
    }

    private void Update()
    {
        animator.SetFloat("Horizontal", Mathf.Clamp(rb.velocity.x, -1, 1));
    }
    public virtual void SetNewEnemyState(EnemyState newState)
    {
        currentState = newState;
    }
    public virtual void ReactToBeingHit(EnemyState stateToReactWith)
    {
        //if (CurrentState != EnemyState.Retreating)
        //{
        //    SetNewEnemyState(EnemyState.Chasing);
        //}
    }
    public void GetPushed()
    {
        Vector3 pushDirection = (transform.position - Player.transform.position).normalized;
        Vector3 newPlayerPos = transform.position + pushDirection * PushForce;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, pushDirection, pushForce, defaultMask);
        if (hit.collider != null)
        {
            return;
        }
        transform.position = newPlayerPos;
    }
}
