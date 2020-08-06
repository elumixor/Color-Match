using UnityEngine;

public class SpeedIncreaser : SingletonBehaviour<SpeedIncreaser> {
    [SerializeField] private AnimationCurve speedCurve;
    [SerializeField] private float curveLength;

    private float elapsed;
    private float speed;

    protected override void Awake() {
        base.Awake();
        Restart();
    }

    public static void Restart() {
        Instance.elapsed = 0f;
        Instance.UpdateSpeed();
    }

    public static float Speed => Instance.speed;

    private void UpdateSpeed() => speed = speedCurve.Evaluate(Mathf.Clamp01(elapsed / curveLength));

    private void Update() {
        elapsed += Time.deltaTime;
        UpdateSpeed();
    }
}