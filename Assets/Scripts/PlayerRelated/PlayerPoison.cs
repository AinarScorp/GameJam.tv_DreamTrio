using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerPoison : MonoBehaviour
{
    [SerializeField] FlashEffect flashScript;
    [SerializeField] [Range(0.1f, 120f)] float damageInterval = 1f;

    [SerializeField] int poisonDamage = 1;

    PlayerHealth player;
    Coroutine playerPoison;
    private void Awake()
    {
        player = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        player.SubscribeToReactHit(ReactToHit);
        playerPoison = StartCoroutine(PoisonPlayer());
    }
    IEnumerator PoisonPlayer()
    {
        while (true)
        {
            if (!player.IsAlive)
            {

                yield return new WaitForSeconds(30f);
                continue;
            }


            yield return new WaitForSeconds(damageInterval);

            if (!player.IsAlive) //since we have a reset now I don't think this is needed
                continue;

            player.ReceiveDamage(poisonDamage, true);
        }
    }
    void ReactToHit()
    {
        StopCoroutine(playerPoison);
        playerPoison = StartCoroutine(PoisonPlayer());

    }
}
