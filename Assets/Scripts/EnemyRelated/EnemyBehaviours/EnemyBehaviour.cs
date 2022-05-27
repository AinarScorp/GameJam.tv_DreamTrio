using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Basic Behaviour Settings")]
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] EnemyState startingState;

    EnemyState currentState;
    PlayerHealth player;
    public EnemyState CurrentState { get => currentState; }
    public PlayerHealth Player { get => player; }

    public virtual void Awake()
    {
        player = FindObjectOfType<PlayerHealth>();

    }
    private void OnDisable()
    {
        //this won't work right now;
        animator.SetFloat("Horizontal", Mathf.Clamp(rb.velocity.x, -1, 1));

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
        if (CurrentState != EnemyState.Retreating)
        {
            SetNewEnemyState(EnemyState.Chasing);
        }
    }
}
