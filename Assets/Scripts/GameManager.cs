using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]RectTransform spaceMessage;
    [SerializeField] TextMeshProUGUI tutorialMessage;
    [Header("Restart Setup")]
    [TextArea]
    [SerializeField] string qustionMessage;
    [TextArea]
    [SerializeField] string restartYesOption;
    [TextArea]
    [SerializeField] string restartNoOption;

    [Header("Victory Setup")]
    [SerializeField] GameObject victoryMenu;
    [TextArea]
    [SerializeField] string victoryMessageInfo;

    bool gameStarted;
    bool gameWon = false;
    private void Start()
    {
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        playerManager.SubscribeToPlayerDied(() => Time.timeScale = 0.5f);
        playerManager.SubscribeToGameOver(GameLostMessage, false);
        playerManager.SubscribeToGameOver(() =>
        {
            VictoryEscape();
            gameWon = true;
        }, true);
        AudioManagerScript.Instance.PlayLoop("Music");
        victoryMenu.SetActive(false);
        spaceMessage.gameObject.SetActive(true);
        tutorialMessage.gameObject.SetActive(true);

    }

    public void SpacePressed()
    {
        if (gameStarted == true)
            return;
        FindObjectOfType<WaveManager>().NextWave();
        tutorialMessage.gameObject.SetActive(false);
        spaceMessage.gameObject.SetActive(false);
        gameStarted = true;

    }
    void VictoryEscape()
    {

        spaceMessage.GetComponent<TextMeshProUGUI>().text = victoryMessageInfo;
        spaceMessage.gameObject.SetActive(true);
    }
    public void EscapePressed()
    {
        tutorialMessage.gameObject.SetActive(false);
        if (!gameWon)
            return;

        spaceMessage.gameObject.SetActive(false);
        victoryMenu.SetActive(true);
        Time.timeScale = 1f;

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
