using UnityEngine;

public class QuitManager : MonoBehaviour {
    public void QuitApplication() {
        // Di Editor, log dulu supaya tahu tombol berfungsi
        Debug.Log("Keluar Aplikasi!");

        // Keluar aplikasi
        Application.Quit();
    }
}
