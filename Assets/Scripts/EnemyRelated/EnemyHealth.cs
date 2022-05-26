using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyDrop))]
public class EnemyHealth : MonoBehaviour
{

    [Header("Adjustment")]
    [SerializeField] int startingHealth = 3;
    [SerializeField] float cordIncreaseAmount = 3f;

    [SerializeField] FlashEffect flashScript;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] Animator animator;
    [SerializeField] EnemyDrop enemyDrop;
    [SerializeField] EnemyMovement enemyMovement;

    int currentHealth;

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void ReceiveDamage(int amount = 1)
    {
        AudioManagerScript.Instance.PlayRandomPitch("Hit");
        hitParticle.Play();
        VirtualCamera.Instance.LightAttackShake();

        if (currentHealth <= 0)
        {
            return;
        }

        flashScript.StartFlash();

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            StartCoroutine( Die());
        }
    }


    IEnumerator Die()
    {
        enemyMovement.enabled = false;
        animator.SetTrigger("Death");
        AudioManagerScript.Instance.Play("Enemy Death");
        enemyDrop.DropStuffUponDeath();
        FindObjectOfType<CordCircle>()?.IncreaseCordLength(cordIncreaseAmount);
        FindObjectOfType<WaveManager>()?.RemoveFromWaveCount();
        while (animator.GetBool("IsDying"))
        {
            yield return null;
        }
        this.gameObject.SetActive(false);


    }

}
