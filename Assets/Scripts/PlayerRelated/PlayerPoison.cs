using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerPoison : MonoBehaviour
{
    [SerializeField] [Range(0.01f,100f)] float damageInterval = 1f;
    [SerializeField] int poisonDamage = 1;

    PlayerHealth player;

    private void Awake()
    {
        player = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        StartCoroutine(PoisonPlayer());
    }
    IEnumerator PoisonPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(damageInterval);

            if (player.IsAlive)
                player.ReceiveDamage(poisonDamage);


        }
    }

}
