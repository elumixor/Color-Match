using System;
using DefaultNamespace;
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
        instance.elapsed = 0f;
        instance.UpdateSpeed();
    }

    public static float Speed => instance.speed;

    private void UpdateSpeed() => speed = speedCurve.Evaluate(Mathf.Clamp01(elapsed / curveLength));

    private void Update() {
        elapsed += Time.deltaTime;
        Debug.Log(speed);
        UpdateSpeed();
    }
}