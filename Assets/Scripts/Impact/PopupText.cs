using System.Collections;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace Impact {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(TextMeshPro))]
    public class PopupText : MonoBehaviour {
        [SerializeField] private float fadeTime;
        [SerializeField] private AnimationCurve opacity;

        [MinMaxSlider(0, 5), SerializeField] private Vector2 up;
        [MinMaxSlider(-2, 2), SerializeField] private Vector2 side;
        [MinMaxSlider(0, 10000), SerializeField] private Vector2 force;
        [MinMaxSlider(0, 5), SerializeField] private Vector2 torque;

        private TextMeshPro text;
        private Rigidbody2D rb;

        private IEnumerator Start() {
            rb = GetComponent<Rigidbody2D>();
            text = GetComponent<TextMeshPro>();

            var elapsed = 0f;
            var color = text.color;

            var directedForce = ((Vector2)transform.position + Vector2.up * up.RandomRange() + Vector2.right * side.RandomRange()).normalized;
            rb.AddForce(directedForce * force.RandomRange());
            rb.AddTorque(Mathf.Abs(torque.RandomRange()) * -directedForce.x, ForceMode2D.Impulse);

            while (elapsed < fadeTime) {
                color.a = opacity.Evaluate(elapsed / fadeTime);
                text.color = color;
                elapsed += Time.deltaTime;
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}