using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeAnimator : SingletonBehaviour<VolumeAnimator> {
    [SerializeField] private Volume effects;

    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float duration;

    public static void Animate() => Instance.StartCoroutine(Instance.Coroutine());

    private IEnumerator Coroutine() {
        var elapsed = 0f;

        while (elapsed < duration) {
            effects.weight = curve.Evaluate(elapsed / duration);
            elapsed += Time.deltaTime;

            yield return null;
        }

        effects.weight = curve.Evaluate(1f);
    }
}