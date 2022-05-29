using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionAnimation : MonoBehaviour
{

    void FinishTransition()
    {
        FindObjectOfType<SceneHandler>().FininshTransitionAnimation();
    }
}
