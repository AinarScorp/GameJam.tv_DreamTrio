using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerGhostMovement ghost;

    PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }
    void Start()
    {
        playerHealth.SubscribeToPlayerDeathPermanently(TurnIntoGhost);
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
