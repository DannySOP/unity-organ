using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Question {
    public string questionText;
    public string[] options;
    public int correctAnswerIndex;
}

public class QuizManager : MonoBehaviour {
    [Header("Quiz Data")]
    public Question[] questions;

    private int currentQuestionIndex = 0;
    private int score = 0;

    [Header("UI Elements")]
    public TMP_Text txtQuestion;
    public TMP_Text txtScore;
    public Slider sliderProgress;
    public TMP_Text txtFinalScore;
    public TMP_Text txtAward;
    public GameObject panelResult;
    public GameObject panelQuiz;
    public TMP_Text txtWelcome;

    // TOMBOL-TOMBOL JAWABAN
    public Button[] answerButtons;

    private void Start() {
        string currentUser = PlayerPrefs.GetString("currentUser", "User");
        txtWelcome.text = "Selamat datang, " + currentUser + "!";

        panelResult.SetActive(false);
        panelQuiz.SetActive(true);

        sliderProgress.maxValue = questions.Length;
        sliderProgress.value = 0;

        DisplayQuestion();
        UpdateScore();
    }

    public void SelectAnswer(int index)   // 👈 FUNGSI INI!
    {
        // Reset semua warna tombol
        foreach (Button btn in answerButtons) {
            btn.image.color = Color.white;
            btn.interactable = true;
        }

        // Warna tombol yang dipilih
        answerButtons[index].image.color = Color.green;

        // Disable semua tombol
        foreach (Button btn in answerButtons) {
            btn.interactable = false;
        }

        // Cek jawaban
        if (index == questions[currentQuestionIndex].correctAnswerIndex) {
            score += 10;
        }

        currentQuestionIndex++;

        // Tunggu sebentar sebelum next soal
        Invoke(nameof(NextQuestion), 1.0f);
    }

    void NextQuestion() {
        if (currentQuestionIndex >= questions.Length) {
            ShowResult();
        } else {
            DisplayQuestion();
            foreach (Button btn in answerButtons) {
                btn.image.color = Color.white;
                btn.interactable = true;
            }
        }
    }

    void DisplayQuestion() {
        Question q = questions[currentQuestionIndex];
        txtQuestion.text = q.questionText;

        for (int i = 0; i < answerButtons.Length; i++) {
            TMP_Text btnText = answerButtons[i].GetComponentInChildren<TMP_Text>();
            btnText.text = q.options[i];
        }

        sliderProgress.value = currentQuestionIndex;
        UpdateScore();
    }

    void UpdateScore() {
        txtScore.text = "Skor: " + score.ToString();
    }

    void ShowResult() {
        panelQuiz.SetActive(false);
        panelResult.SetActive(true);

        txtFinalScore.text = "Skor Akhir: " + score.ToString();

        if (score >= questions.Length * 10 * 0.8f) {
            txtAward.text = "🏅 Gold!";
        } else if (score >= questions.Length * 10 * 0.5f) {
            txtAward.text = "🥈 Silver!";
        } else {
            txtAward.text = "🥉 Bronze!";
        }
    }

    public void Retry() {
        currentQuestionIndex = 0;
        score = 0;
        panelResult.SetActive(false);
        panelQuiz.SetActive(true);
        sliderProgress.value = 0;
        DisplayQuestion();
        UpdateScore();
    }

    public void Logout() {
        SceneManager.LoadScene("LoginScene");
    }
}
