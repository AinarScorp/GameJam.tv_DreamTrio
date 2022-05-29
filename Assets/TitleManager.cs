using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TitleManager : SceneHandler
{
    [Header("Confirmation Exit Setup")]
    [TextArea]
    [SerializeField] string confirmationMessage;





    public override void QuitGame()
    {

        ConfirmationMessage.Instance.ConfirmationQuestion(confirmationMessage, () => 
        {
            Application.Quit();
        }, null);
    }
}
