using Common;
using Player;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TouchParticlesSpawner : SingletonBehaviour<TouchParticlesSpawner> {
    [SerializeField] private ParticleSystem system;

    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
#if UNITY_EDITOR || UNITY_WEBGL
        if (Input.GetMouseButtonDown(0)) {
            var isRight = Input.mousePosition.x > Screen.width * 0.5f;
            var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;
            var particles = Instantiate(system);
            particles.transform.position = worldPosition;
            var colorOrder = PlayerController.Color.order;
            colorOrder = (CollisionColor.Count + colorOrder + (isRight ? 1 : -1)) % CollisionColor.Count;
            var color = CollisionColor.byInt[colorOrder];
            var main = particles.main;
            main.startColor = color.color;
            audioSource.Play();
        }
#else
#endif
    }
}