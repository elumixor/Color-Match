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

    public bool Swap { get; set; }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            var isRight = Input.mousePosition.x > Screen.width * 0.5f;
            var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;
            var particles = Instantiate(system);
            particles.transform.position = worldPosition;
            var colorOrder = PlayerController.Color.order;
            colorOrder = (CollisionColor.Count + colorOrder + (isRight ? 1 : -1)  * (Swap ? 1 : -1)) % CollisionColor.Count;
            var color = CollisionColor.ByInt[colorOrder];
            var main = particles.main;
            main.startColor = color.color;
            audioSource.Play();
        }
    }
}