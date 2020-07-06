using Common;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour {
    [SerializeField] private ParticleSystem deathParticleSystem;

    private float velocity;
    public CollisionColor collisionColor;
    
    private static readonly int ColorShader = Shader.PropertyToID("_Color");

    public void SpawnParticles() {
        var particlesMain = Instantiate(deathParticleSystem, transform.position, Quaternion.identity).main;
        particlesMain.startColor = new ParticleSystem.MinMaxGradient(collisionColor.color);
    }
    
    private void OnCollisionEnter2D(Collision2D _) {
        CameraShaker.Shake();

        VolumeAnimator.Animate();
        SpawnParticles();

        Destroy(gameObject);
    }

    public static Enemy Spawn(Enemy enemyPrefab, Transform parent, CollisionColor collisionColor, float startVelocity) {
        var instance = Instantiate(enemyPrefab, parent);
        instance.GetComponent<Renderer>().material.SetColor(ColorShader, collisionColor.color);

        var rb = instance.GetComponent<Rigidbody2D>();
        
        // Rotate with random angular velocity
        rb.angularVelocity = (Random.Range(0, 2) * 2 - 1) * Random.Range(90f, 180f);
        
        // Assign speed of falling
        rb.velocity = Vector2.down * startVelocity;
        return instance;
    }
}