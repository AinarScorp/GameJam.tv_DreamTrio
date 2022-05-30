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
        AudioManagerScript.Instance.Play("Button");
        SceneManager.LoadScene(2);

    }


    public override void QuitGame()
    {
        AudioManagerScript.Instance.Play("Button");
        ConfirmationMessage.Instance.ConfirmationQuestion(confirmationMessage, () => 
        {
            AudioManagerScript.Instance.Play("Button");
            Application.Quit();
        }, null);
    }
}
