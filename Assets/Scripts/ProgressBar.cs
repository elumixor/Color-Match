using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class ProgressBar : MonoBehaviour {
    [SerializeField] private RoundedButton inner;
    [SerializeField] private RoundedButton outer;
    [SerializeField] private RoundedButton fill;
    [SerializeField] private float thickness;
    [SerializeField] private Color outerColor;
    [SerializeField] private Color fillColor;
    [SerializeField] private Color innerColorLeft;
    [SerializeField] private Color innerColorRight;

    [Range(0, 1)] public float progress;

    private static readonly int ColorBottomID = Shader.PropertyToID("_ColorBottom");
    private static readonly int ColorTopID = Shader.PropertyToID("_ColorTop");
    private static readonly int AngleID = Shader.PropertyToID("_Angle");

    [Button]
    public void UpdateButton() {
        var outerTransform = outer.transform;
        var innerTransform = inner.transform;
        var fillTransform = fill.transform;

        outerTransform.localScale = Vector3.one;
        innerTransform.localScale = Vector3.one;
        fillTransform.localScale = Vector3.one;

        var sizeDelta = GetComponent<RectTransform>().sizeDelta;

        outer.height = sizeDelta.y;
        outer.width = sizeDelta.x;

        fill.height = inner.height = sizeDelta.y - thickness * 2;
        inner.width = sizeDelta.x * progress - thickness * 2;

        fill.width = outer.width - thickness * 2;

        outerTransform.localPosition = Vector3.zero;
        fillTransform.localPosition = Vector3.back;
        innerTransform.localPosition = (outer.width - inner.width - thickness * 2) * .5f * Vector3.left + Vector3.back * 2;

        outer.borderRadius = outer.height * .5f;
        fill.borderRadius = inner.borderRadius = inner.height * .5f;

        outer.GenerateMesh();
        inner.GenerateMesh();
        fill.GenerateMesh();

        outer.GetComponent<Renderer>().sharedMaterial.color = outerColor;
        fill.GetComponent<Renderer>().sharedMaterial.color = fillColor;

        var innerMaterial = inner.GetComponent<Renderer>().sharedMaterial;
        innerMaterial.SetColor(ColorTopID, innerColorLeft);
        innerMaterial.SetColor(ColorBottomID, innerColorRight);

        innerMaterial.SetFloat(AngleID, 90);
    }

#if UNITY_EDITOR
    private void Update() {
        UpdateButton();
    }
#else
    private void Start() {
        UpdateButton();
    }
#endif
}