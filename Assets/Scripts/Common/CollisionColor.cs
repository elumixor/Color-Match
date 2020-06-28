using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common {
    [Serializable]
    public struct CollisionColor {
        public static readonly CollisionColor Orange = new CollisionColor(new Color(1f, 0.62f, 0.05f), 0);
        public static readonly CollisionColor Blue = new CollisionColor(new Color(0.05f, 0.66f, 1f), 1);
        public static readonly CollisionColor Purple = new CollisionColor(new Color(1f, 0.14f, .76f), 2);
        public static readonly CollisionColor Green = new CollisionColor(new Color(0.25f, 0.81f, 0.41f), 3);

        public readonly Color color;
        public readonly int order;

        public CollisionColor(Color color, int order) {
            this.color = color;
            this.order = order;
        }

        public CollisionColor Next => byInt[(order + 1) % 4];
        public CollisionColor Previous => byInt[(4 + order - 1) % 4];
        public CollisionColor Flip => byInt[(order + 2) % 4];
        public static CollisionColor Random => byInt[UnityEngine.Random.Range(0, 4)];


        public static Dictionary<int, CollisionColor> byInt = new Dictionary<int, CollisionColor>
            {{0, Orange}, {1, Blue}, {2, Purple}, {3, Green}};

        public static int operator -(CollisionColor a, CollisionColor b) => a.order - b.order;

        public static bool operator ==(CollisionColor a, CollisionColor b) => a.order == b.order;

        public static bool operator !=(CollisionColor a, CollisionColor b) => !(a == b);

        public bool Equals(CollisionColor other) => order == other.order;

        public override bool Equals(object obj) => obj is CollisionColor other && Equals(other);

        public override int GetHashCode() => order;

        public override string ToString() => $"{order} {color}";
    }
}