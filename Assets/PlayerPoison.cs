using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoison : MonoBehaviour
{
    EnemyHealth enemyHealth;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }


}
