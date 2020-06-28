using System;
using System.Collections;
using Common;
using UnityEngine;

namespace Player {
    public class PlayerController : MonoBehaviour {
        [SerializeField] private ScoreLabel scoreLabel;
        [SerializeField, Range(10e-5f, 10f)] private float rotationTime = 1f;

        private CollisionColor color = CollisionColor.Orange;
        public CollisionColor Color => color;

        private float delta;
        private float startTime;
        private float endTime;
        private float endAngle;
        private bool rotating = false;

        private float CurrentAngle {
            get => transform.localRotation.eulerAngles.y;
            set {
                var angles = transform.localRotation.eulerAngles;
                transform.localRotation = Quaternion.Euler(angles.x, value, angles.z);
            }
        }

        private void Awake() {
            endAngle = CurrentAngle;
        }

        public void RotateLeft() {
            color = color.Next;
            Rotate(-90f);
        }

        public void RotateRight() {
            color = color.Previous;
            Rotate(90f);
        }

        public void Flip() {
            color = color.Flip;
            Rotate(180f);
        }

        private void Rotate(float angle) {
            startTime = Time.time;
            endTime = Time.time + rotationTime;
            delta = (endAngle - CurrentAngle + angle) / rotationTime;
            endAngle = (360 + endAngle + angle) % 360;
            rotating = true;

            scoreLabel.Color = color.color;
        }

        public void ResetRotation() {
            rotating = false;
            color = CollisionColor.Orange;
            CurrentAngle = endAngle = 180f;
            scoreLabel.Color = color.color;
        }

        private void Update() {
            if (!rotating) return;

            var time = Time.time;
            var elapsed = time - startTime;
            var angle = delta * elapsed;

            if (time >= endTime) {
                CurrentAngle = endAngle;
                rotating = false;
                return;
            }

            startTime = time;
            CurrentAngle += angle;
        }
    }
}