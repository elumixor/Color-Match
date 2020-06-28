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

    private bool firstStart = true;
    private bool hidden;

    public bool Hidden {
        set {
            hidden = value;

            if (firstStart) {
                foreach (var h in hideStart) h.SetActive(!hidden);

                if (hidden)
                    firstStart = false;
            } else
                foreach (var h in toggleWinLose)
                    h.SetActive(!hidden);
        }
    }

    private void Update() {
        if (!hidden && Input.GetMouseButtonDown(0)) GameController.DeathScreen = false;
    }
}