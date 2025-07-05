using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundManager : MonoBehaviour {
    public AudioClip clickSound;
    public AudioSource audioSource;

    private void Awake() {
        if (audioSource == null) {
            Debug.LogError("AudioSource belum di-assign!");
            return;
        }

        Button[] allButtons = FindObjectsOfType<Button>();
        foreach (Button btn in allButtons) {
            btn.onClick.AddListener(() => PlayClickSound());
        }
    }

    private void PlayClickSound() {
        if (clickSound != null) {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
