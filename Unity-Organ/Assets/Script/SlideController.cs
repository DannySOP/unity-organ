using UnityEngine;

public class SlideRotator : MonoBehaviour
{
    public GameObject[] slides;
    private int currentIndex = 0;
    public float rotationDuration = 0.5f;
    public float slideSpacing = 10000f; // Jarak antar slide secara horizontal

    void Start()
    {
        UpdateSlidePositions();
    }

    public void NextSlide()
    {
        int previousIndex = currentIndex;
        currentIndex = (currentIndex + 1) % slides.Length;

        for (int i = 0; i < slides.Length; i++)
        {
            Vector3 targetPos = GetTargetPosition(i);
            float targetRotY = GetTargetRotationY(i);

            LeanTween.moveLocal(slides[i], targetPos, rotationDuration).setEaseOutQuad();
            LeanTween.rotateY(slides[i], targetRotY, rotationDuration).setEaseOutBack();
        }
    }

    void UpdateSlidePositions()
    {
        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].SetActive(true);
            slides[i].transform.localPosition = GetTargetPosition(i);
            slides[i].transform.localRotation = Quaternion.Euler(0, GetTargetRotationY(i), 0);
        }
    }

    Vector3 GetTargetPosition(int index)
    {
        int relativeIndex = (index - currentIndex + slides.Length) % slides.Length;

        if (relativeIndex == 0) return new Vector3(0, 0, 0); // Tengah
        else if (relativeIndex == 1) return new Vector3(slideSpacing, 0, 0); // Kanan
        else return new Vector3(-slideSpacing, 0, 0); // Kiri
    }

    float GetTargetRotationY(int index)
    {
        int relativeIndex = (index - currentIndex + slides.Length) % slides.Length;

        if (relativeIndex == 0) return 0;   // Tengah
        else if (relativeIndex == 1) return -15; // Kanan miring
        else return 15;  // Kiri miring
    }
}
