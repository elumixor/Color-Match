using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float spawnTime = 5f;
    [SerializeField] private float startVelocity = 50f;
    

    private float lastSpawnTime;

    private void Start() {
        lastSpawnTime = Time.time - spawnTime;
    }

    private void Update() {
        if (lastSpawnTime + spawnTime <= Time.time) {
            Spawn();
            lastSpawnTime = Time.time;
        }
    }

    private void Spawn() {
        var instance = Instantiate(enemyPrefab, transform);
        instance.Color = CollisionColor.Random;
        instance.GetComponent<Rigidbody2D>().velocity = Vector2.down * startVelocity;
    }

    public void Restart() {
        // Destroy current enemies
        lastSpawnTime = Time.time - spawnTime;
    }
}