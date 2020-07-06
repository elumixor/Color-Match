using System;
using UnityEngine;

namespace DefaultNamespace {
    public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
        protected static T instance;
        protected virtual void Awake() {
            instance = GetComponent<T>();
        }

        public static bool Enabled {
            get => instance.enabled;
            set => instance.enabled = value;
        }
    }
}