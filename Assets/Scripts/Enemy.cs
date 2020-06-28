using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private CollisionColor collisionColor;
    private static readonly int ColorShader = Shader.PropertyToID("_Color");

    public CollisionColor Color {
        get => collisionColor;
        set {
            collisionColor = value;
            GetComponent<Renderer>().material.SetColor(ColorShader, collisionColor.color);
        }
    }

    public event Action OnDestroyed;

    private void OnDestroy() {
        OnDestroyed?.Invoke();
    }
}