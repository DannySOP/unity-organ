using UnityEngine;
using UnityEngine.UI;

public class ProgressButtonManager : MonoBehaviour {
    public Slider progressBar;
    public GameObject unlockObject;  // Object yang akan aktif kalau progress penuh
    public Button[] progressButtons;

    private bool[] buttonClicked;    // Status apakah button sudah pernah diklik
    private int currentProgress = 0;

    private void Start() {
        if (progressBar == null || unlockObject == null || progressButtons.Length == 0) {
            Debug.LogError("ProgressButtonManager: Assign semua reference di inspector!");
            enabled = false;
            return;
        }

        buttonClicked = new bool[progressButtons.Length];
        progressBar.maxValue = progressButtons.Length;
        progressBar.value = 0;

        unlockObject.SetActive(false);

        for (int i = 0; i < progressButtons.Length; i++) {
            int index = i;  // local copy untuk closure
            progressButtons[i].onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    private void OnButtonClicked(int index) {
        if (buttonClicked[index])
            return;  // Sudah pernah ditekan

        buttonClicked[index] = true;
        currentProgress++;
        progressBar.value = currentProgress;

        if (currentProgress >= progressButtons.Length) {
            unlockObject.SetActive(true);
        }
    }

    public void ResetProgress() {
        currentProgress = 0;
        progressBar.value = 0;
        unlockObject.SetActive(false);

        for (int i = 0; i < buttonClicked.Length; i++) {
            buttonClicked[i] = false;
        }
    }
}
