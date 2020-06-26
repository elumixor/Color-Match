using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private Enemy enemyPrefab;

    private void Start() {
        Spawn();
    }

    private void Spawn() {
        var instance = Instantiate(enemyPrefab, transform);
        instance.Color = new System.Random().NextEnum<CollisionColor>();
        instance.GetComponent<Rigidbody2D>().velocity = Vector2.down * 50;
        instance.OnDestroyed += Spawn;
    }
}