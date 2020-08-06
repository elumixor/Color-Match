using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Camera))]
public class CameraShaker : SingletonBehaviour<CameraShaker> {
    [SerializeField] private float duration;
    [SerializeField] private float strength;
    [SerializeField] private AnimationCurve curve;

    private IEnumerator coroutine;

    private Vector3 startPos;

    protected override void Awake() {
        base.Awake();
        startPos = transform.position;
    }

    public static void Shake() {
        if (Instance.coroutine != null) Instance.StopCoroutine(Instance.coroutine);

        Instance.coroutine = Instance.ShakeCoroutine();
        Instance.StartCoroutine(Instance.coroutine);
    }

    private IEnumerator ShakeCoroutine() {
        var elapsed = 0f;

        while (elapsed < duration) {
            var s = curve.Evaluate(elapsed / duration) * strength;

            var x = startPos.x + Random.Range(-s, s);
            var y = startPos.y + Random.Range(-s, s);

            transform.localPosition = new Vector3(x, y, startPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = startPos;
    }

    public new static bool Enabled {
        get => SingletonBehaviour<VolumeAnimator>.Enabled;
        set {
            SingletonBehaviour<VolumeAnimator>.Enabled = value;
            if (!value && Instance.coroutine != null) {
                Instance.StopCoroutine(Instance.coroutine);
                Instance.transform.localPosition = Instance.startPos;
            }
        }
    }
}