using System;
using UnityEditor;
using UnityEngine;

namespace Editor {
    [CustomEditor(typeof(PlayerAutoController))]
    public class PlayerAutoControllerEditor : UnityEditor.Editor {
        private PlayerAutoController autoController;
        private SerializedProperty reactionDistance;

        private void OnEnable() {
            autoController = target as PlayerAutoController;
            reactionDistance = serializedObject.FindProperty(nameof(reactionDistance));
        }

        private void OnSceneGUI() {
            Handles.color = Color.white;
            Handles.DrawWireDisc(autoController.transform.position,  Vector3.forward, reactionDistance.floatValue);
        }
    }
}