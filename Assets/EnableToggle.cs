using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableToggle : StateMachineBehaviour {
    [SerializeField] private bool enabled;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var canvasGroup = animator.GetComponent<CanvasGroup>();
        canvasGroup.interactable = enabled;
        canvasGroup.blocksRaycasts = enabled;
    }
}