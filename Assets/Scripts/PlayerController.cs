using UnityEngine;

public class PlayerController : MonoBehaviour {
    private CollisionColor color = CollisionColor.Orange;
    public CollisionColor Color => color;

    public void RotateLeft() {
        color = color.Next;
        Rotate(-90f);
    }

    public void RotateRight() {
        color = color.Previous;
        Rotate(90f);
    }

    public void Flip() {
        color = color.Flip;
        Rotate(180f);
    }

    private void Rotate(float angle) {
        var angles = transform.localRotation.eulerAngles;
        transform.localRotation = Quaternion.Euler(0, angles.y + angle, 0);
    }

    public void ResetRotation() {
        color = CollisionColor.Orange;
        transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
}