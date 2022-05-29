using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneHandler : MonoBehaviour
{
    bool transitionAnimPlaying = false;
    [SerializeField] Animator transition;
    private void Awake()
    {
        if (transition == null)
        {
            transition = FindObjectOfType<TransitionAnimation>().GetComponent<Animator>();
        }
    }
    public void GoToMainMenu()
    {
        Debug.Log("main menu booooo");
    }

    public void StartFirstLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentScene + 1;
        StartCoroutine(StartTransition(nextSceneIndex));

    }
    public void GoToCreatorScreen()
    {

    }


    IEnumerator StartTransition(int sceneIndex)
    {
        transitionAnimPlaying = true;
        transition.SetTrigger("TransitionOpening");
        while (transitionAnimPlaying)
            yield return null;
        LoadScene(sceneIndex);
    }

    void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public virtual void QuitGame()
    {
        Application.Quit();
    }
    public void FininshTransitionAnimation() => transitionAnimPlaying = false;

}
