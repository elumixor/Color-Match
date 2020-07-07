using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using Common;
using Player;
using UnityEngine;
using Random = System.Random;

public class EnemySpawner : SingletonBehaviour<EnemySpawner> {
    [SerializeField] private Enemy enemyPrefab;

    [SerializeField] private SpeedReactor frequency;
    [SerializeField] private SpeedReactor startVelocity;

    private float nextSpawnTime;
    private CollisionColor lastSpawnColor;

    private void Start() {
        nextSpawnTime = Time.time;
        lastSpawnColor = CollisionColor.Random;
    }

    private void Update() {
        if (Time.time > nextSpawnTime) {
            Spawn();
            nextSpawnTime = Time.time + frequency;
        }
    }

    private void Spawn() {
        var c = CollisionColor.Random;
        if (c.order == lastSpawnColor.order) c = CollisionColor.byInt[(c.order + 1) % CollisionColor.Count];

        lastSpawnColor = c;
        Enemy.Spawn(enemyPrefab, transform, c, startVelocity);
    }
}