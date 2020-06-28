using System;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(PlayerController))]
    public class PlayerCollisionHandler : MonoBehaviour {
        private Animator animator;
        public event Action<Enemy> OnPassed;
        public event Action<Enemy> OnCollided;

        private PlayerController playerController;

        public bool Dead {
            set { animator.SetBool("Dead", value); }
        }

        private void Awake() {
            playerController = GetComponent<PlayerController>();
            animator = GetComponent<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Enemy")) {
                var enemy = other.gameObject.GetComponent<Enemy>();

                var enemyColor = enemy.collisionColor;
                if (enemyColor == playerController.Color)
                    OnPassed?.Invoke(enemy);
                else {
                    OnCollided?.Invoke(enemy);
                }
            }
        }
    }
}