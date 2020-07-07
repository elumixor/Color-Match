using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraShaker : SingletonBehaviour<CameraShaker> {
    [SerializeField] private float duration;
    [SerializeField] private float strength;
    [SerializeField] private AnimationCurve curve;

    public static void Shake() => instance.StartCoroutine(instance.ShakeCoroutine());

    private IEnumerator ShakeCoroutine() {
        var pos = transform.localPosition;

        var elapsed = 0f;

        while (elapsed < duration) {
            var s = curve.Evaluate(elapsed / duration) * strength;

            var x = pos.x + Random.Range(-s, s);
            var y = pos.y + Random.Range(-s, s);

            transform.localPosition = new Vector3(x, y, pos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = pos;
    }
}