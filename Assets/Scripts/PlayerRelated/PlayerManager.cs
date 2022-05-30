using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script is more like a gamemanager script at this point
public class PlayerManager : MonoBehaviour
{

    PlayerGhostMovement ghost;
    PlayerAnimator playerAnimator;


    Action PlayerDeathImmidiateActions;
    Action PlayerRevivalImmidiateActions;

    Action ActivateGhostControls;
    Action ActivateLivingControls;
    Action PlayerDiedForReal;
    Action GameOverFailure;
    Action GameOverVictory;


    int collectedHearths;
    int collectedFireBalls;

    public PlayerAnimator Player { get => playerAnimator; }

    private void Awake()
    {
        playerAnimator = FindObjectOfType<PlayerAnimator>();
        ghost = FindObjectOfType<PlayerGhostMovement>();
    }

    public void StartTurningIntoGhost() => StartCoroutine(TurnIntoGhost());
    public void StartTurningIntoLiving() => StartCoroutine(TurnIntoAliveForm());
    public void StartGameOver(bool hasWon)
    {
        // move it to Game manager
        if (hasWon)
        {
            // turn off pause Manager
            GameOverVictory();
        }
        else
        {
            StartCoroutine(GameLost());
        }

    }
    IEnumerator TurnIntoGhost()
    {
        PlayerDeathImmidiateActions();
        playerAnimator.SetTrigger("HasDied");
        while (playerAnimator.AnimationIsPlaying)
        {
            yield return new WaitForSeconds(0.5f);
        }

        StartCoroutine(AudioManagerScript.Instance.FadeOut("Music", 0.05f, false));
        AudioManagerScript.Instance.Play("Ghost Atmos");
        StartCoroutine(AudioManagerScript.Instance.FadeIn("Ghost Atmos", 0.1f));


        yield return FindObjectOfType<CordCircle>().EncircleTarget();

        AudioManagerScript.Instance.Play("Ghost Appear");
        ghost.transform.position = playerAnimator.transform.position;
        ghost.gameObject.SetActive(true);
        while (ghost.AnimationIsPlaying)
        {
            yield return new WaitForSeconds(0.5f);
        }
        ActivateGhostControls();
    }
    IEnumerator TurnIntoAliveForm()
    {
        PlayerRevivalImmidiateActions();
        ghost.GhostVanish();
        playerAnimator.SetTrigger("Revive");

        while (playerAnimator.AnimationIsPlaying)
        {
            yield return new WaitForSeconds(0.5f);
        }
        AudioManagerScript.Instance.Play("Revive");
        StartCoroutine(AudioManagerScript.Instance.FadeOut("Ghost Atmos", 0f, true));
        StartCoroutine(AudioManagerScript.Instance.FadeIn("Music", 0.2f));


        ActivateLivingControls();
    }
    IEnumerator GameLost()
    {
        PlayerDiedForReal();
        ghost.GhostVanish();
        while (ghost.AnimationIsPlaying)
        {
            yield return new WaitForSeconds(0.5f);

        }
        GameOverFailure();
    }

    #region subscriptions
    public void SubscribeToImmidiateActions(Action actionToAdd, bool toDeath)
    {
        if (toDeath)
        {
            PlayerDeathImmidiateActions += actionToAdd;
            return;
        }
        PlayerRevivalImmidiateActions += actionToAdd;
    }
    public void SubscribeToActivateControls(Action actionToAdd, bool ofGhost)
    {
        if (ofGhost)
        {
            ActivateGhostControls += actionToAdd;
            return;
        }

        ActivateLivingControls += actionToAdd;
    }
    public void UnsubscribeFromImmidiateActions(Action actionToRemove, bool fromDeath)
    {
        if (fromDeath)
        {
            PlayerDeathImmidiateActions -= actionToRemove;
            return;
        }
        PlayerRevivalImmidiateActions -= actionToRemove;
    }
    public void UnsubscribeFromActivateControls(Action actionToRemove, bool ofGhost)
    {
        if (ofGhost)
        {
            ActivateGhostControls -= actionToRemove;
            return;
        }
        ActivateLivingControls -= actionToRemove;
    }
    public void SubscribeToGameOver(Action actionToAdd, bool playerWon)
    {
        if (playerWon)
        {
            GameOverVictory += actionToAdd;
            return;
        }
        GameOverFailure += actionToAdd;
    }
    public void SubscribeToPlayerDied(Action actionToAdd)
    {

        PlayerDiedForReal += actionToAdd;
    }


    #endregion
    #region pickUps
    public void AddCollectedHearth(int amount = 1) => collectedHearths += amount;
    public int GetCollectedHearts()
    {
        int numberToReturn = collectedHearths;
        collectedHearths = 0;
        return numberToReturn;
    }

    public void AddCollectedFireBall(int amount = 1) => collectedFireBalls += amount;
    public int GetCollectedFireBalls()
    {
        int numberToReturn = collectedFireBalls;
        collectedFireBalls = 0;
        return numberToReturn;
    }
    #endregion

}
