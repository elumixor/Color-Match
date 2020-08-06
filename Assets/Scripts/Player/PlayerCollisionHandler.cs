using System;
using Impact;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(PlayerController))]
    public class PlayerCollisionHandler : SingletonBehaviour<PlayerCollisionHandler> {
        [SerializeField] private ParticleSystemForceField forceField;

        public static event Action<Enemy> OnPassed = delegate { };
        public static event Action<Enemy> OnCollided = delegate { };
        
        public static void Restart() {
            Instance.forceField.gravity = new ParticleSystem.MinMaxCurve(1f);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("Enemy")) {
                var enemy = other.gameObject.GetComponent<Enemy>();

                var enemyColor = enemy.collisionColor;
                if (enemyColor == PlayerController.Color) {
                    SoundManager.PlayEnemyCollision();
                    MatchSpawner.Spawn();
                    OnPassed(enemy);
                } else {
                    SoundManager.PlayPlayerDestroyed();
                    Instance.forceField.gravity = new ParticleSystem.MinMaxCurve(-1f);
                    OnCollided(enemy);
                }
            }
        }
    }
}