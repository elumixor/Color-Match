using System;
using System.Collections;
using Common;
using DefaultNamespace;
using UnityEditor.Recorder.Input;
using UnityEngine;

namespace Player {
    public class PlayerController : SingletonBehaviour<PlayerController> {
        public static CollisionColor Color {
            get {
                var angle = instance.CurrentAngle;
                return angle > 315f ? CollisionColor.Pink :
                    angle > 225f ? CollisionColor.Green :
                    angle > 135f ? CollisionColor.Orange :
                    angle > 45f ? CollisionColor.Blue : CollisionColor.Pink;
            }
        }

        public static void RotateLeft() {
            instance.Rotate(-90f);
        }

        public static void RotateRight() {
            instance.Rotate(90f);    
        }

        public static void ResetRotation() {
            instance.rotating = false;
            instance.rotation = instance.CurrentAngle = instance.endAngle = instance.initialRotation;
        }

        [SerializeField, Range(10e-5f, 10f)] private float rotationTime = 0.1f;

        private float delta;
        private float startTime;
        private float endTime;
        private float endAngle;
        private bool rotating;
        private float rotation;
        private float initialRotation;

        private float CurrentAngle {
            get => transform.localRotation.eulerAngles.y;
            set {
                var angles = transform.localRotation.eulerAngles;
                transform.localRotation = Quaternion.Euler(angles.x, value, angles.z);
            }
        }

        protected override void Awake() {
            base.Awake();
            initialRotation = CurrentAngle;
            endAngle = rotation = initialRotation;
        }


        private void Rotate(float angle) {
            startTime = Time.time;
            endTime = Time.time + rotationTime;
            delta = (endAngle - rotation + angle) / rotationTime;
            endAngle += angle;
            rotating = true;
        }

        private void Update() {
            if (!rotating) return;

            var time = Time.time;
            var elapsed = time - startTime;
            var angle = delta * elapsed;

            if (time >= endTime) {
                CurrentAngle = rotation = endAngle;
                rotating = false;
                return;
            }

            startTime = time;
            rotation += angle;
            CurrentAngle = rotation;
        }
    }
}