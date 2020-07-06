using System;
using DefaultNamespace;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(PlayerController))]
    public class PlayerCollisionHandler : SingletonBehaviour<PlayerCollisionHandler> {
        private Animator animator;
        private static readonly int DeadID = Animator.StringToHash("Dead");
        public static event Action<Enemy> OnPassed = delegate { };
        public static event Action<Enemy> OnCollided = delegate { };

        public bool Dead {
            set => animator.SetBool(DeadID, value);
        }

        protected override void Awake() {
            base.Awake();
            animator = GetComponent<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Enemy")) {
                var enemy = other.gameObject.GetComponent<Enemy>();

                var enemyColor = enemy.collisionColor;
                if (enemyColor == PlayerController.Color)
                    OnPassed(enemy);
                else
                    OnCollided(enemy);
            }
        }
    }
}