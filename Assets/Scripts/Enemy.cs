using System;
using Common;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour {
    [SerializeField] private ParticleSystem deathParticleSystem;
    
    [SerializeField] private float minTimeToSpawnParticles = .5f;
    
    public CollisionColor collisionColor;

    private float velocity;

    private static readonly int ColorShader = Shader.PropertyToID("_Color");

    public void SpawnParticles() {
        if (Time.time - spawnTime < minTimeToSpawnParticles) return;
        
        var particlesMain = Instantiate(deathParticleSystem, transform.position, Quaternion.identity).main;
        particlesMain.startColor = new ParticleSystem.MinMaxGradient(collisionColor.color);
    }

    private void OnCollisionEnter2D(Collision2D _) {
        CameraShaker.Shake();

        VolumeAnimator.Animate();
        SpawnParticles();

        Destroy(gameObject);
    }

    private float spawnTime;

    private void Start() {
        spawnTime = Time.time;
    }

    public static Enemy Spawn(Enemy enemyPrefab, Transform parent, CollisionColor collisionColor, float startVelocity) {
        var instance = Instantiate(enemyPrefab, parent);
        instance.GetComponent<Renderer>().material.SetColor(ColorShader, collisionColor.color);
        var main = instance.GetComponent<ParticleSystem>().main;
        main.startColor = collisionColor.color;
        
        instance.collisionColor = collisionColor;

        var rb = instance.GetComponent<Rigidbody2D>();

        // Rotate with random angular velocity
        rb.angularVelocity = (Random.Range(0, 2) * 2 - 1) * Random.Range(90f, 180f);

        // Assign speed of falling
        rb.velocity = Vector2.down * startVelocity;
        return instance;
    }
}