using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TutorialAnimationController : MonoBehaviour
{
    private Animator animator;
    private bool tutorialVisible;

    private void Start()
    {
        this.animator = this.GetComponent<Animator>();
    }

    public void ToggleTutorial()
    {
        if(this.tutorialVisible)
        {
            this.animator.ResetTrigger("Show");
            this.animator.SetTrigger("Hide");
        }
        else
        {
            this.animator.ResetTrigger("Hide");
            this.animator.SetTrigger("Show");
        }
        this.tutorialVisible = !this.tutorialVisible;
    }
}
