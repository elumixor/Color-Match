using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Hide elements on click, when in menu, Send event to GameController to start the game
// 
public class MenuHandler : MonoBehaviour {
    // Elements to hide when in game
    [SerializeField] private List<GameObject> hideStart;
    [SerializeField] private List<GameObject> toggleWinLose;

    // Items for animation
    [SerializeField] private Animator tapIcon;
    [SerializeField] private Animator swipeLeft;
    [SerializeField] private Animator swipeRight;
    [SerializeField] private Animator gameLabel;
    [SerializeField] private Animator scoreLabel;
    [SerializeField] private Animator tryAgainLabel;


    private bool firstStart = true;
    private bool hidden;
    
    private static readonly int Displayed = Animator.StringToHash("Displayed");
    private static readonly int Pressed = Animator.StringToHash("Pressed");
    private static readonly int Hidden = Animator.StringToHash("Hidden");

    private void FirstClick() {
        tapIcon.SetTrigger(Pressed);
        swipeLeft.SetTrigger(Hidden);
        swipeRight.SetTrigger(Hidden);

        gameLabel.SetBool(Displayed, false);
        scoreLabel.SetBool(Displayed, false);

        hidden = true;
    }

    public void Show() {
        gameLabel.SetBool(Displayed, true);
        scoreLabel.SetBool(Displayed, true);
        tryAgainLabel.SetBool(Displayed, true);
        
        hidden = false;
    }

    public void Hide() {
        if (firstStart) {
            FirstClick();
            firstStart = false;
            return;
        }

        tryAgainLabel.SetBool(Displayed, false);
        gameLabel.SetBool(Displayed, false);
        scoreLabel.SetBool(Displayed, false);
        
        hidden = true;
    }

    private void Update() {
        if (!hidden && Input.GetMouseButtonDown(0)) GameController.DeathScreen = false;
    }
}