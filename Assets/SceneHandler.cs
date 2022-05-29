using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneHandler : MonoBehaviour
{
    bool transitionAnimPlaying = false;
    [SerializeField] Animator transition;
    Coroutine animationTrans;

    private void Awake()
    {
        if (transition == null)
        {
            transition = FindObjectOfType<TransitionAnimation>().GetComponent<Animator>();
        }
    }
    public void GoToMainMenu()
    {
        if (animationTrans != null)
        {
            StopCoroutine(animationTrans);
        }
        animationTrans =StartCoroutine(StartTransition(0));
    }

    public void StartFirstLevel()
    {
        if (animationTrans != null)
        {
            StopCoroutine(animationTrans);
        }
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentScene + 1;
        animationTrans =StartCoroutine(StartTransition(nextSceneIndex));

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
        SceneManager.LoadScene(sceneIndex);
    }



    public virtual void QuitGame()
    {
        Application.Quit();
    }
    public void FininshTransitionAnimation() => transitionAnimPlaying = false;

}
