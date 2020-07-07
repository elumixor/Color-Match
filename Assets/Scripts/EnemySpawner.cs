using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using Common;
using DefaultNamespace;
using Player;
using UnityEngine;

public class EnemySpawner : SingletonBehaviour<EnemySpawner> {
    [SerializeField] private Enemy enemyPrefab;

    [SerializeField] private SpeedReactor frequency;
    [SerializeField] private SpeedReactor startVelocity;

    private float nextSpawnTime;

    private void Start() {
        nextSpawnTime = Time.time;
    }

    private void Update() {
        if (Time.time > nextSpawnTime) {
            Spawn();
            nextSpawnTime = Time.time + frequency;
        }
    }

    private void Spawn() => Enemy.Spawn(enemyPrefab, transform, CollisionColor.Random, startVelocity);
}