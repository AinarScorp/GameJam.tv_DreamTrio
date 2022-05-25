using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerHealth : MonoBehaviour
{

    [Header("Adjustment")]
    [SerializeField] int startingHealth = 5;
    [SerializeField] float invulnerabilityTime = 1f;

    [Header("Fool around, delete later")]
    [SerializeField] int currentHealth;

    [Header("Better not touch")]
    [SerializeField] GameObject heartImage;
    [SerializeField] FlashEffect flashScript;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] ParticleSystem poisonParticle;



    int collectedHearths;
    bool isInvincible;
    bool isAlive =true;

    List<GameObject> heartImages = new List<GameObject>();


    Transform parentForHearts;
    PlayerManager playerManager;


    public bool IsAlive { get => isAlive; }
    public int CurrentHealth { get => currentHealth; }


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
        if (isInvincible && !fromPoison)
            return;

        if (!fromPoison)
            AudioManagerScript.Instance.Play("Player Damage");



        VirtualCamera.Instance.PlayerDamageShake(fromPoison);
        PlayParticle(fromPoison);
        flashScript.StartFlash(fromPoison);


        if (currentHealth <= 0) //not needed later
            return;

        RemoveHeartImage();

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            playerManager.StartTurningIntoGhost();
            return;
        }
        StartCoroutine(SetInvinsibility());

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

    IEnumerator SetInvinsibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvincible = false;
    }
    
    public void Revive()
    {
        currentHealth = collectedHearths;
        collectedHearths = 0;
        if (currentHealth <= 0)
        {
            Debug.LogWarning("you lost");
            return;
        }
        playerManager.StartTurningIntoLiving();


    }


    void ToggleIsAlive() => isAlive = !isAlive;

    public void AddCollectedHearth(int amount = 1) => collectedHearths += amount;

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
