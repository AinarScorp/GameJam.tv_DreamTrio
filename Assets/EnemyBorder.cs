using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyBorder : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == enemyLayer)
        {
            EnemyBehaviour enemyBehaviour = collision.gameObject.GetComponent<EnemyBehaviour>();

            if (enemyBehaviour !=null)
            {
                enemyBehaviour.SetNewEnemyState(enemyBehaviour.BorderState);
            }
        }
    }
}
