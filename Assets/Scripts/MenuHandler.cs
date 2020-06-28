using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Hide elements on click, when in menu, Send event to GameController to start the game
// 
public class MenuHandler : MonoBehaviour {
    // Elements to hide when in game
    [SerializeField] private List<GameObject> hideables;

    private bool hidden;

    public bool Hidden {
        set {
            hidden = value;
            foreach (var h in hideables) h.SetActive(!hidden);
        }
    }


    private void Update() {
        if (!hidden && Input.GetMouseButtonDown(0)) GameController.Simulation = false;
    }
}