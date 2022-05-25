using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerGhostMovement ghost;

    PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        ghost = FindObjectOfType<PlayerGhostMovement>();
    }
    void Start()
    {
        playerHealth.SubscribeToDeath(TurnIntoGhost);
        playerHealth.SubscribeToRevival(TurnIntoAliveForm);

    }


    void TurnIntoGhost()
    {

        ghost.transform.position = playerHealth.transform.position;
        ghost.gameObject.SetActive(true);
    }
    void TurnIntoAliveForm()
    {

        ghost.gameObject.SetActive(false);

    }

}
