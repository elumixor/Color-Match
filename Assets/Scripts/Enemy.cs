using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour {
    [SerializeField] private float fadeInTime;
    [SerializeField] private ParticleSystem deathParticleSystem;
    
    public float velocity = 50f;

    public CollisionColor collisionColor;
    private float startTime;
    private bool fading = true;
    
    private static readonly int ColorShader = Shader.PropertyToID("_Color");

    private void Start() {
        var c = collisionColor.color;
        c.a = 0f;
        GetComponent<Renderer>().material.SetColor(ColorShader, c);
        startTime = Time.time;
        
        var rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = (Random.Range(0, 2) * 2 - 1) * Random.Range(90f, 180f);
        rb.velocity = Vector2.down * velocity;
    }

    private void Update() {
        if (!fading) return;
        
        var color = collisionColor.color;
        var delta = Mathf.Clamp01((Time.time - startTime) / fadeInTime);
        color.a = delta;
        GetComponent<Renderer>().material.SetColor(ColorShader, color);

        if (delta >= 1f) {
            fading = false;
        }
    }

    private void OnDestroy() {
        var transform1 = transform;
        var particles = Instantiate(deathParticleSystem, transform1.position, Quaternion.identity);
        var particlesMain = particles.main;
        particlesMain.startColor = new ParticleSystem.MinMaxGradient(collisionColor.color);
    }
}