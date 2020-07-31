using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    [ExecuteInEditMode]
    public class LevelUp : SingletonBehaviour<LevelUp> {
        [SerializeField] private Color colorTop;
        [SerializeField] private Color colorBottom;
        [SerializeField] private Image image;
        [SerializeField] private AnimationCurve timeSlowCurve;
        [SerializeField] private float fadeTime;
        [SerializeField] private float fadeAt;
        [SerializeField] private TextMeshProUGUI newLevelLabel;

        private static readonly int ColorBottomID = Shader.PropertyToID("_ColorBottom");
        private static readonly int ColorTopID = Shader.PropertyToID("_ColorTop");

        private void Update() {
            image.material.SetColor(ColorTopID, colorTop);
            image.material.SetColor(ColorBottomID, colorBottom);
        }

        public static void Show(int newLevel) {
            var elapsed = 0f;
            Instance.newLevelLabel.text = newLevel.ToString();

            var displayedAnimationToggle = Instance.GetComponent<DisplayedAnimationToggle>();
            var startedFading = false;
            var fdt = Time.fixedDeltaTime;
            IEnumerator Coroutine() {
                while (elapsed < Instance.fadeTime) {
                    if (elapsed > Instance.fadeAt && !startedFading) {
                        displayedAnimationToggle.Displayed = false;
                        startedFading = true;
                    }

                    Time.timeScale = Instance.timeSlowCurve.Evaluate(elapsed / Instance.fadeTime);
                    Time.fixedDeltaTime = Time.timeScale * fdt;
                    elapsed += Time.unscaledDeltaTime;
                    yield return null;
                }

                Time.timeScale = Instance.timeSlowCurve.Evaluate(1f);
                Time.fixedDeltaTime = Time.timeScale * fdt;
            }

            displayedAnimationToggle.Displayed = true;
            Instance.StartCoroutine(Coroutine());
        }
    }
}