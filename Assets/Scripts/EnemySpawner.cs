using Common;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : SingletonBehaviour<EnemySpawner> {
    [SerializeField] private Enemy enemyPrefab;

    [SerializeField] private SpeedReactor frequency;
    [SerializeField] private SpeedReactor startVelocity;

    [Range(0, 1), SerializeField] private float doubleTapProbability = .25f;


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

    private void Spawn() {
        var pc = PlayerController.Color.order;
        var next = CollisionColor.ByInt[
            (CollisionColor.Count + pc + (Random.value < doubleTapProbability ? 2 : Random.value > .5f ? 1 : -1)) % CollisionColor.Count];
        Enemy.Spawn(enemyPrefab, transform, next, startVelocity);
    }
}