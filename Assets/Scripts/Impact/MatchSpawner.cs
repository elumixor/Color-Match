using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using XP;

namespace Impact {
    public class MatchSpawner : SingletonBehaviour<MatchSpawner> {
        [SerializeField] private PopupText text;
        [SerializeField] private XPPopup xp;
        [MinMaxSlider(0, 25), SerializeField] private Vector2 radiusThickness;
        [MinMaxSlider(-360, 360), SerializeField] private Vector2 angle;


        public static void Spawn() {
            var r = Instance.radiusThickness.RandomRange();
            var a = Instance.angle.RandomRange() * Mathf.Deg2Rad;
            var p = new Vector3(r * Mathf.Cos(a), r * Mathf.Sin(a), 5);
            Instantiate(Instance.text, Instance.transform).transform.localPosition = p;


            r = Instance.radiusThickness.RandomRange();
            a = 180 - a;
            p = new Vector3(-p.x, r * Mathf.Sin(a), 5);
            Instantiate(Instance.xp, Instance.transform).transform.localPosition = p;
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(MatchSpawner))]
        private class Editor : UnityEditor.Editor {
            private MatchSpawner spawner;

            private void OnEnable() {
                spawner = (MatchSpawner) target;
            }

            private void OnSceneGUI() {
                var transformPosition = spawner.transform.position;
                Handles.CircleHandleCap(0, transformPosition, Quaternion.identity, spawner.radiusThickness.x, EventType.Repaint);
                Handles.CircleHandleCap(0, transformPosition, Quaternion.identity, spawner.radiusThickness.y, EventType.Repaint);
            }
        }
#endif
    }
}