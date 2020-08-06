using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeAnimator : SingletonBehaviour<VolumeAnimator> {
    [SerializeField] private Volume effects;

    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float duration;

    private bool animating;

    protected override void Awake() {
        base.Awake();
        effects.enabled = false;
    }

    public static void Animate() {
        Instance.StartCoroutine(Instance.Coroutine());
    }

    private IEnumerator Coroutine() {
        var elapsed = 0f;
        effects.enabled = true;
        animating = true;

        while (elapsed < duration) {
            effects.weight = curve.Evaluate(elapsed / duration);
            elapsed += Time.deltaTime;

            yield return null;
        }

        effects.weight = curve.Evaluate(1f);
        effects.enabled = false;
        animating = false;
    }

    public new static bool Enabled {
        get => SingletonBehaviour<VolumeAnimator>.Enabled;
        set {
            SingletonBehaviour<VolumeAnimator>.Enabled = value;
            Instance.effects.enabled = value && Instance.animating;
        }
    }
}