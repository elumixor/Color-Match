using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DisplayedAnimationToggle : MonoBehaviour {
    [SerializeField] private string parameterName = "Displayed";
    [SerializeField] private bool invert;

    [SerializeField] private Animator animator;


    // Auto assign component
    private void Reset() => animator = GetComponent<Animator>();


    public bool Displayed {
        get => animator.GetBool(parameterName);
        set => animator.SetBool(parameterName, invert ? !value : value);
    }
}