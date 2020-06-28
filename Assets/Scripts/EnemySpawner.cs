using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using Common;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField, Range(1e-3f, 2f)] private float spawnFrequency = 1f;
    [SerializeField, Range(1f, 500f)] private float startVelocity = 50f;
    [SerializeField, Range(0.1f, 1f)] private float increaseFactor = 0.9999f;

    private float frequency;
    private float lastSpawnTime;

    private void Awake() {
        frequency = spawnFrequency;
    }

    private void Start() {
        lastSpawnTime = Time.time - spawnFrequency;
    }

    private void Update() {
        frequency *= increaseFactor;
        if (lastSpawnTime + spawnFrequency <= Time.time) {
            Spawn();
            lastSpawnTime = Time.time;
        }
    }

    private void Spawn() {
        var instance = Instantiate(enemyPrefab, transform);
        instance.collisionColor = CollisionColor.Random;
        instance.velocity = startVelocity;
    }

    public void Restart() {
        // Destroy current enemies
        lastSpawnTime = Time.time - spawnFrequency;
        frequency = spawnFrequency;
    }
}