using System;
using UnityEngine;
using UnityEngine.Events;

namespace UI {
    public class SettingsOption : MonoBehaviour {
        [SerializeField] private Animator yes;
        [SerializeField] private Animator no;

        [SerializeField] private UnityEvent onYes;
        [SerializeField] private UnityEvent onNo;

        private static readonly int SelectedHash = Animator.StringToHash("Selected");

        public event Action<bool> OnChanged = delegate { };

        public void SelectOption(bool value) {
            OnChanged(value);

            if (value)
                onYes.Invoke();
            else
                onNo.Invoke();
            
            yes.SetBool(SelectedHash, value);
            no.SetBool(SelectedHash, !value);
        }
    }
}