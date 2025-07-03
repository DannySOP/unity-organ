using UnityEngine;
using UnityEngine.UI;

public class RotateController : MonoBehaviour
{
    public Transform paruParuObject;
    public Slider rotateSlider;

    private void Start()
    {
        if (rotateSlider != null)
        {
            rotateSlider.minValue = 0f;
            rotateSlider.maxValue = 360f;
            rotateSlider.onValueChanged.AddListener(OnSliderChanged);
        }
    }

    public void OnSliderChanged(float value)
    {
        if (paruParuObject != null)
        {
            paruParuObject.rotation = Quaternion.Euler(0f, value, 0f);
        }
    }

    public void ResetRotation() {
        if (paruParuObject != null) {
            paruParuObject.rotation = Quaternion.identity;
            rotateSlider.value = 0f; // default rotasi
        }
    }
}
