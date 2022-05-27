using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    [SerializeField] EnemyBehaviour behaviour;

    int currentHealth;
    Action reactToHit;
    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void ReceiveDamage(int amount = 1)
    {
        AudioManagerScript.Instance.PlayRandomPitch("Hit");
        hitParticle.Play();
        VirtualCamera.Instance.LightAttackShake();


        flashScript.StartFlash();

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
            return;
        }
        reactToHit();
    }


    IEnumerator Die()
    {
        behaviour.SetNewEnemyState(EnemyState.Dying);
        GetComponent<Collider2D>().enabled = false;

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
    public void SubscribeToReactHit(Action actionToAdd)
    {
        reactToHit += actionToAdd;
    }
}
