using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerPoison : MonoBehaviour
{
<<<<<<< HEAD
    [SerializeField] FlashEffect flashScript;
    [SerializeField] ParticleSystem poisonParticle;
    [SerializeField] [Range(1, 120f)] float damageInterval = 2f;
=======
    [SerializeField] [Range(0.01f,100f)] float damageInterval = 1f;
>>>>>>> 9017dc5ed367fb11137d61d0ce20153476c86315
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
            {
                poisonParticle.Play();
                AudioManagerScript.Instance.Play("Poison Damage");
                flashScript.StartPoisonFlash();
                player.ReceiveDamage(poisonDamage);
            }


        }
    }

}
