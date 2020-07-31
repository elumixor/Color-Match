using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Impact {
    [RequireComponent(typeof(TextMeshPro))]
    public class RandomTextSelector : MonoBehaviour {
        [SerializeField] private List<string> matchTexts;
        private void Start() => GetComponent<TextMeshPro>().text = matchTexts.Random();
        
    }
}