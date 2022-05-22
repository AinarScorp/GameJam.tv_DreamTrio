using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerHealth : MonoBehaviour
{
    [Header("Adjustment")]
    [SerializeField] int startingHealth = 5;

    [Header("Fool around, delete later")]
    [SerializeField] int currentHealth;

    public int CurrentHealth { get => currentHealth; }

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void ReceiveDamage(int amount = 1)
    {
        if (currentHealth <= 0)
            return;
        Debug.Log("ouch it hurts");

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }

    }



    void Die()
    {

        FindObjectOfType<CordCircle>().EncircleTarget(this.transform);

        EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();

        enemies.ToList().ForEach(enemy => enemy.TogglePlayerDeath());
        //spawn ghost
        //encircle
        //adjust the circle with the radius stuff
        Debug.Log("player has died");
    }

    void InstaKill()
    {
        ReceiveDamage(currentHealth);
    }
}
