using Common;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class TouchParticlesSpawner : SingletonBehaviour<TouchParticlesSpawner> {
    [SerializeField] private TouchParticle touchParticle;

    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public bool Swap { get; set; }

    public static void Spawn(Vector2 position) {
        var isRight = Input.mousePosition.x > Screen.width * 0.5f;
        var instance = Instantiate(Instance.touchParticle);
        instance.transform.position = new Vector3(position.x, position.y, 5f);
        var colorOrder = PlayerController.Color.order;
        colorOrder = (CollisionColor.Count + colorOrder + (isRight ? 1 : -1) * (Instance.Swap ? 1 : -1)) % CollisionColor.Count;
        var instanceColor = CollisionColor.ByInt[colorOrder].color;
        instance.Color = instanceColor;
        Instance.audioSource.Play();
    }
}