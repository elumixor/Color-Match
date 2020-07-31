using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Player {
    public class PlayerController : SingletonBehaviour<PlayerController> {
        public static CollisionColor Color {
            get {
                var angle = Instance.CurrentAngle;
                return angle > 315f ? CollisionColor.Orange :
                    angle > 225f ? CollisionColor.Green :
                    angle > 135f ? CollisionColor.Pink :
                    angle > 45f ? CollisionColor.Blue : CollisionColor.Orange;
            }
        }

        public static void RotateLeft() {
            Instance.Rotate(-90f);
        }

        public static void RotateRight() {
            Instance.Rotate(90f);
        }

        public static void ResetPlayer() {
            Instance.rotating = false;
            Instance.rotation = Instance.CurrentAngle = Instance.endAngle = Instance.initialRotation;
            Instance.animator.SetBool(DeadID, false);
        }

        [SerializeField] private SpeedReactor rotationTime;

        private static readonly int DeadID = Animator.StringToHash("Dead");

        private float delta;
        private float startTime;
        private float endTime;
        private float endAngle;
        private bool rotating;
        private float rotation;
        private float initialRotation;
        private Animator animator;

        private float CurrentAngle {
            get => transform.localRotation.eulerAngles.z;
            set {
                var angles = transform.localRotation.eulerAngles;
                transform.localRotation = Quaternion.Euler(angles.x, angles.y, value);
            }
        }

        protected override void Awake() {
            base.Awake();
            initialRotation = CurrentAngle;
            endAngle = rotation = initialRotation;
            animator = GetComponent<Animator>();
            animator.SetBool(DeadID, false);
        }

        private void Start() {
            PlayerCollisionHandler.OnCollided += delegate { animator.SetBool(DeadID, true); };
        }


        private void Rotate(float angle) {
            var r = rotationTime;
            startTime = Time.time;
            endTime = Time.time + r;
            delta = (endAngle - rotation + angle) / r;
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