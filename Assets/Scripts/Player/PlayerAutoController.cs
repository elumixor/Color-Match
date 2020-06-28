using System;
using Common;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(PlayerController))]
    public class PlayerAutoController : MonoBehaviour {
        [SerializeField] private float reactionDistance;

        private PlayerController playerController;

        private FixedQueue<Enemy> reactedEnemies = new FixedQueue<Enemy>(10);

        private (Enemy, float) ClosestEnemy {
            get {
                var enemies = FindObjectsOfType<Enemy>();

                Enemy closest = null;
                var distance = float.PositiveInfinity;

                var position = transform.position;
                foreach (var enemy in enemies) {
                    var d = (position - enemy.transform.position).magnitude;
                    if (d < distance) {
                        distance = d;
                        closest = enemy;
                    }
                }

                return (closest, distance);
            }
        }

        private void Awake() {
            playerController = GetComponent<PlayerController>();
        }

        private void Update() {
            var (enemy, distance) = ClosestEnemy;
            if (enemy == null || distance > reactionDistance || reactedEnemies.Contains(enemy)) return;
            ReactToEnemy(enemy);
        }

        private void ReactToEnemy(Enemy enemy) {
            var colorDistance = playerController.Color - enemy.Color;

            if (Math.Abs(colorDistance) == 2) playerController.Flip();
            else {
                if (Math.Abs(colorDistance) > 2) colorDistance /= -3;

                if (colorDistance > 0) playerController.RotateRight();
                else if (colorDistance < 0) playerController.RotateLeft();
            }
        
            reactedEnemies.Enqueue(enemy);
        }
    }
}