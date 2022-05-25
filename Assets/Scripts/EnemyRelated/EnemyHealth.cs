using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyDrop))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] FlashEffect flashScript;
    [SerializeField] ParticleSystem hitParticle;

    [Header("Adjustment")]
    [SerializeField] int startingHealth = 3;
    [SerializeField] float cordIncreaseAmount = 3f;


    [Header("Fool around, delete later")]
    [SerializeField] int currentHealth;


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
            Die();
        }
    }


    void Die()
    {
        AudioManagerScript.Instance.Play("Enemy Death");
        GetComponent<EnemyDrop>().DropStuffUponDeath();
        FindObjectOfType<CordCircle>()?.IncreaseCordLength(cordIncreaseAmount);
        FindObjectOfType<WaveManager>()?.RemoveFromActiveEnemies(this);
        this.gameObject.SetActive(false);


    }




}
