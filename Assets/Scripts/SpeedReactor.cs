using System;

[Serializable]
public class SpeedReactor {
    public float offset;
    public float factor;

    public float Value => SpeedIncreaser.Speed * factor + offset;

    public static implicit operator float(SpeedReactor r) => r.Value;
}