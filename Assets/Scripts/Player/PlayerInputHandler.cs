using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player {
    [RequireComponent(typeof(PlayerController))]
    public class PlayerInputHandler : SingletonBehaviour<PlayerInputHandler> {
        private void Update() {
            for (var i = 0; i < Input.touchCount; i++) {
                var t = Input.GetTouch(i);
                if (t.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(t.fingerId)) {
                    ReactToInput(t.position.x);
                    TouchParticlesSpawner.Spawn(t.position);
                }
            }
// #if UNITY_EDITOR || UNITY_WEBGL
            // if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) ReactToInput(Input.mousePosition.x);
// #else
            // if (Input.touchCount > 0) {
                // var touch = Input.GetTouch(0);
                // if (touch.phase == TouchPhase.Began) ReactToInput(Input.GetTouch(0).position.x);
            // }
// #endif
        }

        public bool Swap { get; set; }

        private void ReactToInput(float x) {
            var isRight = x >= Screen.width / 2f;

            if (Swap) {
                if (isRight) PlayerController.RotateRight();
                else PlayerController.RotateLeft();
            } else {
                if (isRight) PlayerController.RotateLeft();
                else PlayerController.RotateRight();
            }
        }
    }
}