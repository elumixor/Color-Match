using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class TouchParticle : MonoBehaviour {
    [SerializeField] private AnimationCurve followStrength;
    [SerializeField] private AnimationCurve scale;
    [SerializeField] private float lifetime;

    public Color Color {
        set => meshRenderer.material.color = value;
    }

    private MeshRenderer meshRenderer;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private IEnumerator Start() {
        var elapsed = 0f;
        var tr = transform;
        while (elapsed < lifetime) {
            var t = elapsed / lifetime;

            tr.position = Vector3.Lerp(tr.position, Vector3.zero, followStrength.Evaluate(t));
            tr.localScale = scale.Evaluate(t) * Vector3.one;


            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return null;

        Destroy(gameObject);
    }
}