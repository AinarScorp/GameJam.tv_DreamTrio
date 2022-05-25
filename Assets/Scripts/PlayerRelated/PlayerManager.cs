using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    PlayerGhostMovement ghost;
    PlayerAnimator playerAnimator;



    Action PlayerDeathImmidiateActions;
    Action PlayerRevivalImmidiateActions;

    Action ActivateGhostControls;
    Action ActivateLivingControls;



    private void Awake()
    {
        playerAnimator = FindObjectOfType<PlayerAnimator>();
        ghost = FindObjectOfType<PlayerGhostMovement>();
    }

    public void StartTurningIntoGhost() => StartCoroutine(TurnIntoGhost());
    public void StartTurningIntoLiving() => StartCoroutine(TurnIntoAliveForm());

    IEnumerator TurnIntoGhost()
    {
        PlayerDeathImmidiateActions();
        playerAnimator.SetTrigger("HasDied");
        while (playerAnimator.AnimationIsPlaying)
        {
            yield return new WaitForSeconds(0.5f);
        }


        AudioManagerScript.Instance.Play("Ghost Atmos");
        StartCoroutine(AudioManagerScript.Instance.FadeIn("Ghost Atmos"));


        yield return FindObjectOfType<CordCircle>().EncircleTarget();

        AudioManagerScript.Instance.Play("Ghost Appear");
        ghost.transform.position = playerAnimator.transform.position;
        ghost.gameObject.SetActive(true);

        ActivateGhostControls();
    }
    IEnumerator TurnIntoAliveForm()
    {
        PlayerRevivalImmidiateActions();
        playerAnimator.SetTrigger("Revive");
        if (playerAnimator.AnimationIsPlaying)
        {
            yield return new WaitForSeconds(0.5f);
        }
        AudioManagerScript.Instance.Play("Revive");
        StartCoroutine(AudioManagerScript.Instance.FadeOut("Ghost Atmos"));

        ghost.gameObject.SetActive(false);

        ActivateLivingControls();


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

    #endregion
}
