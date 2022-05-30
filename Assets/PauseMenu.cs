using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PauseMenu : MonoBehaviour
{
    bool gameIsPaused = false;
    [Header("Confirmation Quit Setup")]
    [TextArea]
    [SerializeField] string confirmationMessage;
    [TextArea]
    [SerializeField] string quitYesOption;
    [TextArea]
    [SerializeField] string quitNoOption;
    [Header("Tutorial Setup")]


    [SerializeField] GameObject tutorialMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject[] stuffToTurnOff;

    private void Start()
    {
        pauseMenu.SetActive(false);
        tutorialMenu.SetActive(false);

        gameIsPaused = false;
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        playerManager.SubscribeToPlayerDied(() => this.gameObject.SetActive(false));
        playerManager.SubscribeToGameOver(() => this.gameObject.SetActive(false), true);

    }

    public void EscapePressed()
    {
        if (gameIsPaused)
        {
            ResumeTheGame();
        }
        else
        {
            PauseTheGame();
        }
    }
    public void ResumeTheGame()
    {
        AudioManagerScript.Instance.Play("Button");
        tutorialMenu.SetActive(false);

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        stuffToTurnOff.ToList().ForEach(item => item.SetActive(true));
    }
    void PauseTheGame()
    {

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        stuffToTurnOff.ToList().ForEach(item => item.SetActive(false));
    }
    public void TutorialScreen(bool turnOn)
    {
        pauseMenu.SetActive(!turnOn);
        tutorialMenu.SetActive(turnOn);
    }


    public void QuitTheGame()
    {
        AudioManagerScript.Instance.Play("Button");

        ConfirmationMessage.Instance.ConfirmationQuestion(confirmationMessage, () =>
        {
            FindObjectOfType<SceneHandler>().GoToMainMenu();
            pauseMenu.SetActive(false);

        }, null, quitYesOption, quitNoOption);
    }
}
