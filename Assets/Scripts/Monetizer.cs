using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Monetizer : MonoBehaviour {
    [SerializeField] private string googlePlayID = "3692437";
    [SerializeField] private string appStoreID = "3692436";
    [SerializeField] private bool testMode;

    private void Start() {
#if UNITY_ANDROID
        Advertisement.Initialize(googlePlayID, testMode);
#else
        Advertisement.Initialize(appStoreID, testMode);
#endif
    }
}