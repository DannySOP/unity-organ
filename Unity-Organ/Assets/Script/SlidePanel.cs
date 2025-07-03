using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePanel : MonoBehaviour {
    public RectTransform panel;    // Drag panel putih ke sini
    public Vector2 targetPosition; // Posisi tujuan
    public float duration = 0.5f;  // Lama animasi dalam detik

    private Vector2 startPosition;
    private float elapsedTime = 0f;
    private bool isSliding = false;

    void Update() {
        if (isSliding) {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            panel.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);

            if (t >= 1f)
                isSliding = false;
        }
    }

    public void Slide() {
        startPosition = panel.anchoredPosition;
        elapsedTime = 0f;
        isSliding = true;
    }
}
