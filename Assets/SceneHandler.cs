using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneHandler : MonoBehaviour
{
    bool transitionAnimPlaying = false;
    public void GoToMainMenu()
    {
        Debug.Log("main menu booooo");
    }

    public void StartMainLevel()
    {
        StartCoroutine(LoadingMainLevel());

    }


    IEnumerator LoadingMainLevel()
    {
        transitionAnimPlaying = true;
        //transition.SetTrigger("TriggerStartTransition");
        while (transitionAnimPlaying)
            yield return null;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentScene + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
    public void GoToCreatorScreen()
    {

    }
    public virtual void QuitGame()
    {
        Application.Quit();
    }

}
