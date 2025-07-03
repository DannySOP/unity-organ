using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginSystem : MonoBehaviour {
    public TMP_InputField inputUsername;
    public TMP_InputField inputPassword;
    public TMP_Text txtStatus;

    public void Register() {
        string username = inputUsername.text;
        string password = inputPassword.text;

        if (username == "" || password == "") {
            txtStatus.text = "Username dan Password wajib diisi terlebih dahulu! ";
            return;
        }

        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("password", password);
        PlayerPrefs.Save();

        txtStatus.text = "Register berhasil! Silakan login.";
    }

    public void Login() {
        string savedUsername = PlayerPrefs.GetString("username", "");
        string savedPassword = PlayerPrefs.GetString("password", "");

        if (savedUsername == "" || savedPassword == "") {
            txtStatus.text = "Belum ada akun. Silakan register dulu.";
            return;
        }

        if (inputUsername.text == savedUsername && inputPassword.text == savedPassword) {
            // Simpan username agar scene berikut bisa akses
            PlayerPrefs.SetString("currentUser", savedUsername);
            // Pindah scene
            SceneManager.LoadScene("LearningScene");
        } else {
            txtStatus.text = "Username atau Password salah.";
        }
    }
}