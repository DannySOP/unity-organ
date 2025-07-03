using UnityEngine;

public class SlideRotator : MonoBehaviour {
    public GameObject[] slides;
    private int currentIndex = 0;

    [Header("Animation Settings")]
    public float rotationDuration = 0.5f;
    public float slideSpacing = 1000f;

    [Header("Size Settings")]
    public Vector2 activeSize = new Vector2(500f, 500f);
    public Vector2 inactiveSize = new Vector2(400f, 400f);

    void Start() {
        UpdateSlidePositions();
    }

    public void NextSlide() {
        currentIndex = (currentIndex + 1) % slides.Length;

        for (int i = 0; i < slides.Length; i++) {
            Vector3 targetPos = GetTargetPosition(i);
            float targetRotY = GetTargetRotationY(i);

            // Move
            LeanTween.moveLocal(slides[i], targetPos, rotationDuration).setEaseOutQuad();
            // Rotate
            LeanTween.rotateY(slides[i], targetRotY, rotationDuration).setEaseOutBack();

            // Scale via sizeDelta
            RectTransform rt = slides[i].GetComponent<RectTransform>();
            if (rt != null) {
                Vector2 targetSize = (i == currentIndex) ? activeSize : inactiveSize;
                AnimateSize(rt, targetSize, rotationDuration);
            }

            // Alpha transparansi
            CanvasGroup cg = slides[i].GetComponent<CanvasGroup>();
            if (cg != null) {
                float targetAlpha = (i == currentIndex) ? 1f : 0.5f;
                LeanTween.value(slides[i], cg.alpha, targetAlpha, rotationDuration)
                         .setOnUpdate((float val) => {
                             cg.alpha = val;
                         });
            }

            // Urutan hierarchy
            if (i == currentIndex) {
                slides[i].transform.SetAsLastSibling();
            } else {
                slides[i].transform.SetAsFirstSibling();
            }
        }
    }

    void UpdateSlidePositions() {
        for (int i = 0; i < slides.Length; i++) {
            slides[i].SetActive(true);
            slides[i].transform.localPosition = GetTargetPosition(i);
            slides[i].transform.localRotation = Quaternion.Euler(0, GetTargetRotationY(i), 0);

            RectTransform rt = slides[i].GetComponent<RectTransform>();
            if (rt != null) {
                rt.sizeDelta = (i == currentIndex) ? activeSize : inactiveSize;
            }

            CanvasGroup cg = slides[i].GetComponent<CanvasGroup>();
            if (cg != null) {
                cg.alpha = (i == currentIndex) ? 1f : 0.5f;
            }

            if (i == currentIndex) {
                slides[i].transform.SetAsLastSibling();
            } else {
                slides[i].transform.SetAsFirstSibling();
            }
        }
    }

    void AnimateSize(RectTransform rt, Vector2 targetSize, float duration) {
        Vector2 startSize = rt.sizeDelta;
        LeanTween.value(rt.gameObject, 0f, 1f, duration)
            .setOnUpdate((float t) => {
                rt.sizeDelta = Vector2.Lerp(startSize, targetSize, t);
            })
            .setEaseOutBack();
    }

    Vector3 GetTargetPosition(int index) {
        int relativeIndex = (index - currentIndex + slides.Length) % slides.Length;

        if (relativeIndex == 0)
            return new Vector3(0, 0, -50); // Tengah lebih maju
        else if (relativeIndex == 1)
            return new Vector3(slideSpacing, 0, 0); // Kanan
        else
            return new Vector3(-slideSpacing, 0, 0); // Kiri
    }

    float GetTargetRotationY(int index) {
        int relativeIndex = (index - currentIndex + slides.Length) % slides.Length;

        if (relativeIndex == 0) return 0;
        else if (relativeIndex == 1) return -15;
        else return 15;
    }
}
