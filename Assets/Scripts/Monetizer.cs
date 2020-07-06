﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Monetizer : MonoBehaviour {
    [SerializeField] private string googlePlayID = "3692437";
    [SerializeField] private bool testMode;

    private void Start() {
        Advertisement.Initialize(googlePlayID, testMode);
    }
}