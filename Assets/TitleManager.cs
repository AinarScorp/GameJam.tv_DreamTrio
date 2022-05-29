using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [Header("Confirmation Exit Setup")]
    [TextArea]
    [SerializeField] string confirmationMessage;

    public void StartGame()
    {

    }
    public void GoToCreatorScreen()
    {

    }
    public void QuitGame()
    {
        ConfirmationMessage.Instance.ConfirmationQuestion(confirmationMessage, () => 
        {
            Application.Quit();
        }, null);
    }
}
