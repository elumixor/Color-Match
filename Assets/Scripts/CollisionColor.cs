using System.ComponentModel;
using UnityEngine;

public enum CollisionColor {
    Orange,
    Blue,
    Purple,
    Green
}

public static class EnemyColorExtensions {
    public static Color Color(this CollisionColor collisionColor) {
        switch (collisionColor) {
            case CollisionColor.Orange:
                return GameData.Colors.Orange;
            case CollisionColor.Blue:
                return GameData.Colors.Blue;
            case CollisionColor.Purple:
                return GameData.Colors.Purple;
            case CollisionColor.Green:
                return GameData.Colors.Green;
        }

        throw new InvalidEnumArgumentException("Invalid enemy color type");
    }

    public static CollisionColor Next(this CollisionColor collisionColor) {
        switch (collisionColor) {
            case CollisionColor.Orange:
                return CollisionColor.Blue;
            case CollisionColor.Blue:
                return CollisionColor.Purple;
            case CollisionColor.Purple:
                return CollisionColor.Green;
            case CollisionColor.Green:
                return CollisionColor.Orange;
        }

        throw new InvalidEnumArgumentException("Invalid enemy color type");
    }

    public static CollisionColor Previous(this CollisionColor collisionColor) {
        switch (collisionColor) {
            case CollisionColor.Orange:
                return CollisionColor.Green;
            case CollisionColor.Blue:
                return CollisionColor.Orange;
            case CollisionColor.Purple:
                return CollisionColor.Blue;
            case CollisionColor.Green:
                return CollisionColor.Purple;
        }

        throw new InvalidEnumArgumentException("Invalid enemy color type");
    }
    
    
}