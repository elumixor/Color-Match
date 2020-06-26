using System;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerCollisionHandler : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI scoreText;

    private PlayerController playerController;
    private int score = 0;

    private void Awake() {
        playerController = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            var enemyColor = other.gameObject.GetComponent<Enemy>().Color;
            if (enemyColor == playerController.Color) {
                Passed(other.gameObject);
            } else {
                Collided(other.gameObject);
            }
        }
    }

    private void Passed(GameObject enemy) {
        Destroy(enemy);
        score += 1;
        UpdateScoreText();
    }

    private void Collided(GameObject enemy) {
        Destroy(enemy);
        score = 0;
        UpdateScoreText();    
        // Destroy(gameObject);
        playerController.ResetColor();
    }

    private void UpdateScoreText() {
        scoreText.text = score.ToString();
    }
}