using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI {
    public class ScoreLabel : MonoBehaviour {
        [SerializeField] private List<DisplayedAnimationToggle> starsAnimationToggles;
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private DisplayedAnimationToggle labelAnimationToggle;

        private bool displayStars;
        
        public int Score {
            set => label.text = value.ToString();
        }

        public bool Displayed {
            set => labelAnimationToggle.Displayed = value;
        }

        public bool DisplayStars {
            set {
                foreach (var star in starsAnimationToggles) star.Displayed = value;
            }
        }
    }
}