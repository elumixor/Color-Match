using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
    public static T Instance { get; private set; }

    protected virtual void Awake() {
        Instance = GetComponent<T>();
    }

    public static bool Enabled {
        get => Instance.enabled;
        set => Instance.enabled = value;
    }
}