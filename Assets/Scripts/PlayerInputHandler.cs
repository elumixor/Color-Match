using System;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInputHandler : MonoBehaviour {
    private PlayerController playerController;

    private void Awake() {
        playerController = GetComponent<PlayerController>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0))
            playerController.RotateLeft();
        else if (Input.GetMouseButtonDown(1))
            playerController.RotateRight();
        else if (Input.GetMouseButtonDown(2))
            playerController.Flip();
    }
}