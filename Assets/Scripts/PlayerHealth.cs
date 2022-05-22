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
    [SerializeField] GameObject heartImage;

    Transform parentForHearts;
    List<GameObject> heartImages =  new List<GameObject>();
    int collectedHearths;
    public int CurrentHealth { get => currentHealth; }
    private void Awake()
    {
        parentForHearts = GameObject.FindGameObjectWithTag("Heart Container").transform;

    }
    private void Start()
    {
        currentHealth = startingHealth;
        AddHeartImages();
    }

    private void AddHeartImages()
    {
        for (int i = 0; i < currentHealth; i++)
        {
            GameObject newHeartImage = Instantiate(heartImage, parentForHearts);
            heartImages.Add(newHeartImage);
        }
    }
    private void RemoveHeartImage()
    {
        if (heartImages.Count <1)
            return;

        GameObject firstHeartImage = heartImages.ToList().FirstOrDefault();
        heartImages.Remove(firstHeartImage);
        Destroy(firstHeartImage);

    }

    public void ReceiveDamage(int amount = 1)
    {
        if (currentHealth <= 0)
            return;
        RemoveHeartImage();


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
        collectedHearths = 0;
        if (currentHealth <= 0)
        {
            Debug.LogWarning("you lost");
        }
        AddHeartImages();
        GetComponent<PlayerMovement>().enabled = true;
        EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();

        enemies.ToList().ForEach(enemy => enemy.TogglePlayerDeath());

        GetComponent<PlayerBasicAttack>().SetEnabled(true);

    }
}
