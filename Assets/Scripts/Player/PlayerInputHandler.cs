using System;
using DefaultNamespace;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(PlayerController))]
    public class PlayerInputHandler : SingletonBehaviour<MonoBehaviour> {
        private void Update() {
            if (Input.touchCount > 0) {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) {
                    if (Input.GetTouch(0).position.x >= Screen.width / 2f) PlayerController.RotateRight();
                    else PlayerController.RotateLeft();
                }
            }
        }
    }
}