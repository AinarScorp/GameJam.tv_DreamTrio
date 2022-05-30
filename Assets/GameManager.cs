using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]RectTransform spaceMessage;
    [Header("Restart Setup")]
    [TextArea]
    [SerializeField] string qustionMessage;
    [TextArea]
    [SerializeField] string restartYesOption;
    [TextArea]
    [SerializeField] string restartNoOption;

    bool gameStarted;
    private void Start()
    {
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        playerManager.SubscribeToPlayerDied(() => Time.timeScale = 0.5f);
        playerManager.SubscribeToGameOver(GameLostMessage, false);
    }

    public void SpacePressed()
    {
        if (gameStarted == true)
            return;
        FindObjectOfType<WaveManager>().NextWave();
        spaceMessage.gameObject.SetActive(false);
        gameStarted = true;

    }

    public void GameLostMessage()
    {
        SceneHandler sceneHandler = FindObjectOfType<SceneHandler>();

        ConfirmationMessage.Instance.ConfirmationQuestion(qustionMessage, 
            () =>
            {
                sceneHandler.RestartLevel();
            }, () =>
            {
                ConfirmationMessage.Instance.ConfirmationQuestion("Thank you for playing",
                    () =>
                    {
                        sceneHandler.GoToCreatorScreen();

                    }, () =>
                    {
                        sceneHandler.GoToCreatorScreen();

                    }, "It was fun", "Let me die");

            }, restartYesOption, restartNoOption
        );
    }
}
