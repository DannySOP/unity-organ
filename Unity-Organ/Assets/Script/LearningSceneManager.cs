using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LearningSceneManager : MonoBehaviour {
    public TMP_Text txtWelcome;

    private void Start() {
        string currentUser = PlayerPrefs.GetString("currentUser", "User");
        txtWelcome.text =  currentUser;
    }

    public void Logout() {
        // Bisa hapus current user kalau mau
        PlayerPrefs.DeleteKey("currentUser");

        // Kembali ke scene login
        SceneManager.LoadScene("LoginScene");
    }
}