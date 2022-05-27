using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyBorder : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;
    BoxCollider2D boxCollider;
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == enemyLayer)
        {
            EnemyBehaviour enemyBehaviour = GetComponent<EnemyBehaviour>();
            if (enemyBehaviour !=null)
            {
                enemyBehaviour.SetNewEnemyState(EnemyState.Chasing);
            }
        }
    }
}
