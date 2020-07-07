using System;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
    public static T instance;
    protected virtual void Awake() {
        instance = GetComponent<T>();
    }

    public static bool Enabled {
        get => instance.enabled;
        set => instance.enabled = value;
    }
}