using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGear : MonoBehaviour, IUIInteract
{
    [SerializeField] private Animator animator;
    private readonly int _animationOpenUI = Animator.StringToHash("ClickUI");

    private void Start()
    {
        if (animator == null) animator = GetComponent<Animator>();
    }

    public void Click()
    {
        Debug.Log("Click Gear");
        animator.SetTrigger(_animationOpenUI);
    }
}
