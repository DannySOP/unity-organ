using UnityEngine;
using UnityEngine.UI;

public class ZoomController : MonoBehaviour
{
    [Header("Zoom Target")]
    public Transform paruParuContainer;

    [Header("UI")]
    public Slider zoomSlider;

    [Header("Zoom Settings")]
    public float minScale = 0.5f;
    public float maxScale = 2f;
    public float zoomSpeed = 0.5f; // digunakan untuk tombol

    private float currentScale;

    void Start()
    {
        if (zoomSlider == null || paruParuContainer == null)
        {
            Debug.LogError("ZoomController: Referensi belum lengkap!", this);
            enabled = false;
            return;
        }

        zoomSlider.minValue = minScale;
        zoomSlider.maxValue = maxScale;

        currentScale = paruParuContainer.localScale.x;
        zoomSlider.value = currentScale;

        zoomSlider.onValueChanged.AddListener(SetZoomFromSlider);
    }

    public void SetZoomFromSlider(float value)
    {
        currentScale = Mathf.Clamp(value, minScale, maxScale);
        paruParuContainer.localScale = Vector3.one * currentScale;
    }

    public void ZoomIn()
    {
        float newScale = currentScale + zoomSpeed * Time.deltaTime;
        UpdateZoom(newScale);
    }

    public void ZoomOut()
    {
        float newScale = currentScale - zoomSpeed * Time.deltaTime;
        UpdateZoom(newScale);
    }

    private void UpdateZoom(float newScale)
    {
        currentScale = Mathf.Clamp(newScale, minScale, maxScale);
        paruParuContainer.localScale = Vector3.one * currentScale;
        zoomSlider.value = currentScale;
    }

    public void ResetZoom() {
        currentScale = 0.3127224f; // atau nilai default sesuai preferensi kamu
        paruParuContainer.localScale = Vector3.one * currentScale;
        zoomSlider.value = currentScale;
    }
}
