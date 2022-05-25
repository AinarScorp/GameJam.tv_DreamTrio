using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] FlashEffect flashScript;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] ParticleSystem poisonParticle;


    [Header("Adjustment")]
    [SerializeField] int startingHealth = 5;

    [Header("Fool around, delete later")]
    [SerializeField] int currentHealth;

    [Header("Better not touch")]
    [SerializeField] GameObject heartImage;

    int collectedHearths;


    List<GameObject> heartImages = new List<GameObject>();

    Transform parentForHearts;

    bool isAlive;
    Action RevivePlayer;
    Action PlayerDeath;

    public bool IsAlive { get => isAlive; }
    public int CurrentHealth { get => currentHealth; }

    private void Awake()
    {
        parentForHearts = GameObject.FindGameObjectWithTag("Heart Container").transform;
    }
    private void Start()
    {

        currentHealth = startingHealth;
        isAlive = true;
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
        if (heartImages.Count < 1)
            return;

        GameObject firstHeartImage = heartImages.ToList().FirstOrDefault();
        heartImages.Remove(firstHeartImage);
        Destroy(firstHeartImage);

    }

    public void ReceiveDamage(int amount = 1, bool fromPoison = false)
    {
        if (!fromPoison)
            AudioManagerScript.Instance.Play("Player Damage");



        VirtualCamera.Instance.PlayerDamageShake(fromPoison);
        PlayParticle(fromPoison);
        flashScript.StartFlash(fromPoison);


        if (currentHealth <= 0)
            return;
        RemoveHeartImage();

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }

    }

    void PlayParticle(bool isPoison)
    {
        if (isPoison)
        {
            poisonParticle.Play();
            return;
        }
        hitParticle.Play();
    }

    void Die()
    {
        isAlive = false;
        AudioManagerScript.Instance.Play("Ghost Appear");
        AudioManagerScript.Instance.Play("Ghost Atmos");
        StartCoroutine(AudioManagerScript.Instance.FadeIn("Ghost Atmos"));
        EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();

        PlayerDeath();

    }

    void InstaKill()
    {
        ReceiveDamage(currentHealth);
    }


    public void AddCollectedHearth(int amount = 1) => collectedHearths += amount;
    public void Revive()
    {
        AudioManagerScript.Instance.Play("Revive");
        StartCoroutine(AudioManagerScript.Instance.FadeOut("Ghost Atmos"));

        currentHealth = collectedHearths;
        collectedHearths = 0;
        if (currentHealth <= 0)
        {
            Debug.LogWarning("you lost");
        }
        AddHeartImages();
        RevivePlayer?.Invoke();
        isAlive = true;
    }
    public void SubscribeToRevival(Action actionToAdd) => RevivePlayer += actionToAdd;
    public void UnSubscribeFromRevival(Action actionToRemove) => RevivePlayer -= actionToRemove;
    public void SubscribeToDeath(Action actionToAdd) => PlayerDeath += actionToAdd;
    public void UnSubscribeFromDeath(Action actionToRemove) => PlayerDeath -= actionToRemove;

}
