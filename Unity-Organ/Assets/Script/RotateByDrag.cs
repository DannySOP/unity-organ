using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateByDrag : MonoBehaviour {
    public Transform target;          // Objek yang mau diputar
    public float rotationSpeed = 100f;

    private bool isDragging = false;
    private Vector3 lastMousePosition;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0)) {
            isDragging = false;
        }

        if (isDragging) {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            float rotationY = delta.x * rotationSpeed * Time.deltaTime;

            target.Rotate(Vector3.up, -rotationY, Space.World);

            lastMousePosition = Input.mousePosition;
        }
    }

    public void ResetRotation() {
        target.rotation = Quaternion.identity;
    }
}
