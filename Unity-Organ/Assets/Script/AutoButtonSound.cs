using UnityEngine;

public class AutoButtonSound : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip clickSound;

    void Start() {
        if (audioSource == null || clickSound == null) {
            Debug.LogWarning("AutoButtonSound: AudioSource atau Clip belum di-assign!");
            return;
        }

        var buttons = FindObjectsOfType<UnityEngine.UI.Button>();
        foreach (var btn in buttons) {
            btn.onClick.AddListener(PlaySound);
        }
    }

    void PlaySound() {
        audioSource.PlayOneShot(clickSound);
    }
}
