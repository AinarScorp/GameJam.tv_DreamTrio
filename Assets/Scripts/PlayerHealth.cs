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

    [Header("Better not touch")]



    int collectedHearths;
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
        GetComponent<PlayerBasicAttack>().SetEnabled(false);
        GetComponent<PlayerMovement>().TurnIntoGhost();
        //spawn ghost
        //encircle
        //adjust the circle with the radius stuff
    }

    void InstaKill()
    {
        ReceiveDamage(currentHealth);
    }


    public void AddCollectedHearth(int amount = 1) => collectedHearths += amount;
    public void Revive()
    {
        currentHealth = collectedHearths;
        GetComponent<PlayerMovement>().enabled = true;
        EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();

        enemies.ToList().ForEach(enemy => enemy.TogglePlayerDeath());

        GetComponent<PlayerBasicAttack>().SetEnabled(true);

    }
}
