using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerHealth : MonoBehaviour
{

    [Header("Adjustment")]
    [SerializeField] int startingHealth = 5;
    [SerializeField] int maxHealth = 24;
    [SerializeField] float invulnerabilityTime = 1f;


    [Header("Better not touch")]
    [SerializeField] GameObject heartImage;
    [SerializeField] FlashEffect flashScript;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] ParticleSystem poisonParticle;



    int currentHealth;
    bool isInvincible;
    bool isAlive =true;

    List<GameObject> heartImages = new List<GameObject>();


    Transform parentForHearts;
    PlayerManager playerManager;
    Action reactToHit;


    public bool IsAlive { get => isAlive; }
    public int CurrentHealth { get => currentHealth; }
    public bool IsInvincible { get => isInvincible;  }

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        parentForHearts = GameObject.FindGameObjectWithTag("Heart Container").transform;
    }
    private void Start()
    {

        currentHealth = startingHealth;
        isAlive = true;
        AddHeartImages();

        playerManager.SubscribeToImmidiateActions(ToggleIsAlive, true);
        playerManager.SubscribeToActivateControls(AddHeartImages, false);
        playerManager.SubscribeToActivateControls(ToggleIsAlive, false);
        playerManager.SubscribeToActivateControls(SetInvincility, false);

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
        if (isInvincible)
            return;

        if (fromPoison)
        {
            AudioManagerScript.Instance.Play("Poison Damage");
        }
        else
        {
            AudioManagerScript.Instance.Play("Player Damage");
        }



        VirtualCamera.Instance.PlayerDamageShake(fromPoison);
        PlayParticle(fromPoison);


        if (currentHealth <= 0) //not needed later
            return;

        RemoveHeartImage();

        currentHealth -= amount;
        reactToHit();
        if (currentHealth <= 0)
        {
            playerManager.StartTurningIntoGhost();
            return;
        }

        SetInvincility(fromPoison);

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
    void SetInvincility(bool fromPoison)
    {
        flashScript.StartFlash(fromPoison);

        StartCoroutine(SetInvinsibilityTimer());

    }
    void SetInvincility()
    {
        flashScript.StartFlash(true);

        StartCoroutine(SetInvinsibilityTimer());

    }

    IEnumerator SetInvinsibilityTimer()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvincible = false;
    }
    
    public void Revive()
    {
        currentHealth = playerManager.GetCollectedHearts();
        if (currentHealth >maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0)
        {
            StartCoroutine(AudioManagerScript.Instance.FadeOut("Music", 0f, true));
            playerManager.StartGameOver(false);
            Debug.LogWarning("you lost");
            return;
        }
        playerManager.StartTurningIntoLiving();


    }


    void ToggleIsAlive() => isAlive = !isAlive;

    public void SubscribeToReactHit(Action actionToAdd) => reactToHit += actionToAdd;

    #region comments /useless stuff
    /*
     * animation death - player falling
     * 
     * circle find player center
     *      circle expanidning 
     *      
     *               while circle expanding
     *   AudioManagerScript.Instance.Play("Ghost Atmos"); play
     *  StartCoroutine(AudioManagerScript.Instance.FadeIn("Ghost Atmos")); play
     *          camera zooms out
     *          //make hearts different  - ask John - hearticles
     *          
     *          enemies should fuck off
     *          
     *      after full circle expansion 
     * ghost appears 
     *  play ghost appear sound
     * 
     * player ghost gets controls
     * 
     * start shrinking
    */


    //IEnumerator WaitBeforeGhostTime()
    //{
    //    //animator.SetTrigger("HasDied");
    //    //animationIsPlaying = true;

    //    //while (animationIsPlaying)
    //    //{
    //    //    yield return new WaitForSeconds(0.2f);
    //    //}

    //    //AudioManagerScript.Instance.Play("Ghost Atmos");
    //    //StartCoroutine(AudioManagerScript.Instance.FadeIn("Ghost Atmos"));


    //    //yield return FindObjectOfType<CordCircle>().EncircleTarget();
    //}

    //IEnumerator Die()
    //{
    //    yield return WaitBeforeGhostTime();


    //    PlayerDeath();

    //}
    void InstaKill()
    {
        ReceiveDamage(currentHealth);
    }
    #endregion
}
