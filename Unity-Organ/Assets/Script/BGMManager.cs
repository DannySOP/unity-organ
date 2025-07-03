using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.UI;

public class BGMManager : MonoBehaviour {
    public AudioSource audioSource;
    public Slider volumeSlider;

    private void Start() {
        if (audioSource == null || volumeSlider == null) {
            Debug.LogError("BGMManager: Assign AudioSource dan Slider!");
            enabled = false;
            return;
        }

        // Load saved volume or default ke 0.5
        float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        audioSource.volume = savedVolume;
        volumeSlider.value = savedVolume;

        // Event listener
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        // Start play BGM
        audioSource.Play();
    }

    private void OnVolumeChanged(float value) {
        audioSource.volume = value;
        PlayerPrefs.SetFloat("BGMVolume", value);
        PlayerPrefs.Save();
    }
}