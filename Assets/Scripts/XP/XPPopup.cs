using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace XP {
    [RequireComponent(typeof(TextMeshPro))]
    public class XPPopup : MonoBehaviour {
        [MinMaxSlider(0, 10), SerializeField] private Vector2 xp;
        [MinMaxSlider(0, 100), SerializeField] private Vector2 timeMultiplier;
        [MinMaxSlider(0, 100), SerializeField] private Vector2 elapsedMultiplier;

        private void Start() {
            var value = Mathf.RoundToInt(xp.RandomRange() + timeMultiplier.RandomRange() * SpeedIncreaser.Speed +
                                         elapsedMultiplier.RandomRange() * GameController.Elapsed);
            GetComponent<TextMeshPro>().text = $"+{value}xp";
            XPBar.AddXP(value);
        }
    }
}