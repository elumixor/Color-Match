using Common;
using DefaultNamespace;
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

        public static void ResetPlayer() {
            instance.rotating = false;
            instance.rotation = instance.CurrentAngle = instance.endAngle = instance.initialRotation;
            instance.animator.SetBool(DeadID, false);
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