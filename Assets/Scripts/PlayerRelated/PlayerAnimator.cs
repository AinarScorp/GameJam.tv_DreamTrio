using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;

    bool animationIsPlaying;
    public bool AnimationIsPlaying { get => animationIsPlaying; }

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

    }

    public void SetTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }
    void StopAnimationPlaying() => animationIsPlaying = false;

}
