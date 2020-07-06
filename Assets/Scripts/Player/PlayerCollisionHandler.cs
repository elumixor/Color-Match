using System;
using DefaultNamespace;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(PlayerController))]
    public class PlayerCollisionHandler : SingletonBehaviour<PlayerCollisionHandler> {
        public static event Action<Enemy> OnPassed = delegate { };
        public static event Action<Enemy> OnCollided = delegate { };


        protected override void Awake() {
            base.Awake();
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