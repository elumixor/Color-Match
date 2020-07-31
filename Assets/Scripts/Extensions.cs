using System.Collections.Generic;
using UnityEngine;

public static class Extensions {
    public static void SetAngleX(this Transform transform, float x) {
        var angles = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(x, angles.y, angles.z);
    }

    public static void SetAngleY(this Transform transform, float y) {
        var angles = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(angles.x, y, angles.z);
    }

    public static void SetAngleZ(this Transform transform, float z) {
        var angles = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(angles.x, angles.y, z);
    }

    public static float RandomRange(this Vector2 v) => UnityEngine.Random.Range(v.x, v.y);
    public static T Random<T>(this List<T> list) => list[UnityEngine.Random.Range(0, list.Count)];
}