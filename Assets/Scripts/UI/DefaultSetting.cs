using UnityEngine;

namespace UI {
    [RequireComponent(typeof(SettingsOption))]
    public class DefaultSetting : MonoBehaviour {
        [SerializeField] private string prefsKey;

        private void Start() {
            var settingsOption = GetComponent<SettingsOption>();
            settingsOption.SelectOption(PlayerPrefs.GetInt(prefsKey, 0) == 1);
            settingsOption.OnChanged += value => PlayerPrefs.SetInt(prefsKey, value ? 1 : 0);
        }
    }
}