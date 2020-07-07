using System;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(PlayerController))]
    public class PlayerCollisionHandler : SingletonBehaviour<PlayerCollisionHandler> {
        [SerializeField] private ParticleSystemForceField forceField;

        public static event Action<Enemy> OnPassed = delegate { };
        public static event Action<Enemy> OnCollided = delegate { };


        protected override void Awake() {
            base.Awake();
        }

        public static void Restart() {
            instance.forceField.gravity = new ParticleSystem.MinMaxCurve(1f);
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Enemy")) {
                var enemy = other.gameObject.GetComponent<Enemy>();

                var enemyColor = enemy.collisionColor;
                if (enemyColor == PlayerController.Color) {
                    SoundManager.PlayEnemyCollision();
                    OnPassed(enemy);
                } else {
                    SoundManager.PlayPlayerDestroyed();
                    instance.forceField.gravity = new ParticleSystem.MinMaxCurve(-1f);
                    OnCollided(enemy);
                }
            }
        }
    }
}