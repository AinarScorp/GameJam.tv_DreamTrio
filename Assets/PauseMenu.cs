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


    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject[] stuffToTurnOff;

    private void Start()
    {
        pauseMenu.SetActive(false);
        gameIsPaused = false;

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
        if (!Application.isEditor)
            Cursor.visible = false;

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        stuffToTurnOff.ToList().ForEach(item => item.SetActive(true));
    }
    void PauseTheGame()
    {
        if (!Application.isEditor)
            Cursor.visible = true;

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        stuffToTurnOff.ToList().ForEach(item => item.SetActive(false));
    }

    public void QuitTheGame()
    {
        ConfirmationMessage.Instance.ConfirmationQuestion(confirmationMessage, () =>
        {
            FindObjectOfType<SceneHandler>().GoToMainMenu();
            pauseMenu.SetActive(false);

        }, null, quitYesOption, quitNoOption);
    }
}
