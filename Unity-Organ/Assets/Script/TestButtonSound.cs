using UnityEngine;

public class TestButtonSound : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip clickSound;

    public void PlaySFX() {
        audioSource.PlayOneShot(clickSound);
    }
}
