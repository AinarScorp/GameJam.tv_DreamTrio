using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
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
        if (currentHealth <= 0)
            return;
        Debug.Log("I am damaged said: " + name);

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }


    void Die()
    {

        FindObjectOfType<CordCircle>().IncreaseCordLength(cordIncreaseAmount);
        this.gameObject.SetActive(false);


    }
}
