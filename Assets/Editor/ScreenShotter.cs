using System;
using UnityEditor;
using UnityEngine;

namespace Editor {
    public class ScreenShotter : EditorWindow {
        private const string iPhone65Name = "iPhone 6.5";
        private const string iPhone55Name = "iPhone 6.5";
        private const string landscape = "(Landscape)";
        private const string portrait = "(Portrait)";

        private (int width, int height) iphone65Size = new ValueTuple<int, int>(1242, 2688);
        private (int width, int height) iphone55Size = new ValueTuple<int, int>(1242, 2208);

        [MenuItem("Window/Screen Shotter")]
        public static void ShowWindow() {
            GetWindow(typeof(ScreenShotter));
        }

        private void OnEnable() {
            savePath = Application.dataPath + "/Screenshots";
        }

        private string savePath;
        private string screenShotName = "Screenshot";
        private int i;

        private void OnGUI() {
            savePath = GUILayout.TextField(savePath);
            name = GUILayout.TextField(name);
            i = EditorGUILayout.IntField(i);
            
            if (GUILayout.Button("Save")) {
                ScreenCapture.CaptureScreenshot(savePath + $"/{name}_{i}.png");
                i++;
            }
        }
    }
}