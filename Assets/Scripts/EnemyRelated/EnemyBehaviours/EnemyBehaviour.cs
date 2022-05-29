using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyCallibration))]
public class EnemyBehaviour : MonoBehaviour
{
    [Header("Basic Behaviour Settings")]
    [SerializeField] float pushForce = 0.5f;

    [SerializeField] LayerMask defaultMask;
    [SerializeField] EnemyCallibration callibration;
    [SerializeField] Animator animator;
    [SerializeField] EnemyState startingState;
    [SerializeField] EnemyState borderState;

    EnemyState currentState;
    PlayerHealth player;
    public float PushForce { get => pushForce; }
    public EnemyState CurrentState { get => currentState; }
    public EnemyState BorderState { get => borderState;  }
    public Animator Animator { get => animator;  }
    public PlayerHealth Player { get => player; }
    public EnemyCallibration Callibration { get => callibration; }

    public virtual void Awake()
    {
        player = FindObjectOfType<PlayerHealth>();

    }

    private void Start()
    {
        SetNewEnemyState(startingState);
    }

    public virtual void SetNewEnemyState(EnemyState newState)
    {
        callibration.enabled = false;
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
    public void StartCallibration()
    {
        callibration.enabled = true;

    }
}
