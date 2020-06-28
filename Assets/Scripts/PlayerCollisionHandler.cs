using System;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(PlayerController))]
public class PlayerCollisionHandler : MonoBehaviour {
    public event Action<Enemy> OnPassed;
    public event Action<Enemy> OnCollided;

    private PlayerController playerController;

    private void Awake() {
        playerController = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            var enemy = other.gameObject.GetComponent<Enemy>();

            var enemyColor = enemy.Color;
            if (enemyColor == playerController.Color)
                OnPassed?.Invoke(enemy);
            else
                OnCollided?.Invoke(enemy);
        }
    }
}