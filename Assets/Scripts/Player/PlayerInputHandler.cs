using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Player {
    [RequireComponent(typeof(PlayerController))]
    public class PlayerInputHandler : SingletonBehaviour<MonoBehaviour> {
        private void Update() {
#if UNITY_EDITOR || UNITY_WEBGL
            if (Input.GetMouseButtonDown(0)) ReactToInput(Input.mousePosition.x);
#else
            if (Input.touchCount > 0) {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) ReactToInput(Input.GetTouch(0).position.x);
            }
#endif
        }

        private void ReactToInput(float x) {
            var isRight = x >= Screen.width / 2f;

            if (isRight) PlayerController.RotateLeft();
            else PlayerController.RotateRight();
        }
    }
}