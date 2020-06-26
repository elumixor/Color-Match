using UnityEngine;

public class PlayerController : MonoBehaviour {
    private CollisionColor color = CollisionColor.Orange;
    public CollisionColor Color => color;

    private void Update() {
        if (Input.GetMouseButtonDown(0))
            RotateLeft();
        else if (Input.GetMouseButtonDown(1))
            RotateRight();
        else if (Input.GetMouseButtonDown(2)) {
            Flip();
        }
    }

    private void RotateLeft() {
        color = color.Previous();
        Rotate(90f);
    }

    private void RotateRight() {
        color = color.Next();
        Rotate(-90f);
    }

    private void Flip() {
        color = color.Next().Next();
        Rotate(180f);
    }

    private void Rotate(float angle) {
        var angles = transform.localRotation.eulerAngles;
        transform.localRotation = Quaternion.Euler(0, angles.y + angle, 0);
    }

    public void ResetColor() {
        color = CollisionColor.Orange;
        transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
}