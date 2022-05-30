using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : SceneHandler
{
    [Header("Confirmation Exit Setup")]
    [TextArea]
    [SerializeField] string confirmationMessage;

    public override void StartFirstLevel()
    {

        base.StartFirstLevel();
        AudioManagerScript.Instance.Play("Start");

    }
    public override void GoToCreatorScreen()
    {
        SceneManager.LoadScene(2);

    }


    public override void QuitGame()
    {

        ConfirmationMessage.Instance.ConfirmationQuestion(confirmationMessage, () => 
        {
            Application.Quit();
        }, null);
    }
}
