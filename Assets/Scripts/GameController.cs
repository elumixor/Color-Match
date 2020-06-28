using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour {
    private static GameController instance;

    [SerializeField] private MenuHandler menuHandler;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ScoreLabel scoreLabel;
    [SerializeField] private EnemySpawner enemySpawner;

    private PlayerCollisionHandler collisionHandler;
    private PlayerAutoController autoController;
    private PlayerInputHandler inputHandler;

    private bool simulation = true;

    private void Awake() {
        autoController = playerController.GetComponent<PlayerAutoController>();
        inputHandler = playerController.GetComponent<PlayerInputHandler>();
        collisionHandler = playerController.GetComponent<PlayerCollisionHandler>();

        collisionHandler.OnCollided += OnCollided;
        collisionHandler.OnPassed += OnPassed;

        instance = this;
    }

    private void Start() {
        Simulation = true;
    }


    public static bool Simulation {
        get => instance.simulation;
        set {
            instance.simulation = value;
            instance.inputHandler.enabled = !value;
            instance.autoController.enabled = value;
            instance.menuHandler.Hidden = !value;
            instance.scoreLabel.DisplayHighscore = value;

            if (!value) instance.scoreLabel.Score = 0;
            
            // Destroy all enemies, reset player and spawner
            foreach (var enemy in FindObjectsOfType<Enemy>()) Destroy(enemy.gameObject);
            
            instance.playerController.ResetRotation();
            instance.enemySpawner.Restart();
        }
    }

    private void OnPassed(Enemy enemy) {
        Destroy(enemy.gameObject);

        if (!simulation) scoreLabel.Score++;
    }

    private void OnCollided(Enemy enemy) {
        Destroy(enemy.gameObject);

        Simulation = true;
    }
}