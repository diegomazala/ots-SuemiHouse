using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideFurniture : MonoBehaviour
{
    public Animator animator;
    public bool show = true;

    private void Start()
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void Show ()
    {
        animator.SetTrigger("Show");
        show = true;
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
        show = false;
    }

    public void ShowHide(UnityEngine.UI.Toggle toggle)
    {
        show = toggle.isOn;
        if (show)
            animator.SetTrigger("Show");
        else
            animator.SetTrigger("Hide");

    }

    public void ShowHide()
    {
        show = !show;
        if (show)
            animator.SetTrigger("Show");
        else
            animator.SetTrigger("Hide");

    }
}
