using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using Common;
using DefaultNamespace;
using UnityEngine;

public class EnemySpawner : SingletonBehaviour<EnemySpawner> {
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField, Range(1e-3f, 2f)] private float spawnFrequency = 1f;
    [SerializeField, Range(1f, 500f)] private float startVelocity = 50f;
    [SerializeField, Range(0.1f, 1f)] private float increaseFactor = 0.9999f;

    private float frequency;
    private float lastSpawnTime;
    
    protected override void Awake() {
        base.Awake();
        frequency = spawnFrequency;
    }

    private void Start() {
        lastSpawnTime = Time.time - spawnFrequency;
    }

    private void Update() {
        frequency *= increaseFactor;
        if (lastSpawnTime + frequency <= Time.time) {
            Spawn();
            lastSpawnTime = Time.time;
        }
    }

    private void Spawn() => Enemy.Spawn(enemyPrefab, transform, CollisionColor.Random, startVelocity);

    public static void Restart() {
        instance.lastSpawnTime = Time.time - instance.spawnFrequency;
        instance.frequency = instance.spawnFrequency;
    }
}