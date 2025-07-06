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
    public Question[] questionsTopik1;
    public Question[] questionsTopik2;

    private Question[] questions;
    private int currentQuestionIndex = 0;
    private int score = 0;

    [Header("UI Elements")]
    public TMP_Text txtQuestion;
    public TMP_Text txtScore;
    public Slider sliderProgress;
    public TMP_Text txtFinalScore;
    public TMP_Text txtAward;

    public GameObject imageBronze;
    public GameObject imageSilver;
    public GameObject imageGold;

    public Button buttonNext;

    public GameObject panelMainMenu;
    public GameObject panelQuiz;
    public GameObject panelResult;

    public TMP_Text txtWelcome;

    public Button[] answerButtons;

    private int selectedAnswerIndex = -1;
    private bool answerSelected = false;

    [Header("Audio")]
    public AudioSource sfxSource;

    void Start() {
        string currentUser = PlayerPrefs.GetString("currentUser", "User");
        txtWelcome.text = "Selamat datang, " + currentUser + "!";

        // Awal hanya tampilkan menu utama
        panelQuiz.SetActive(false);
        panelResult.SetActive(false);
    }

    public void StartQuizTopik1() {
        StartQuiz(questionsTopik1);
    }

    public void StartQuizTopik2() {
        StartQuiz(questionsTopik2);
    }

    public void StartQuiz(Question[] selectedQuestions) {
        questions = selectedQuestions;
        currentQuestionIndex = 0;
        score = 0;
        answerSelected = false;
        selectedAnswerIndex = -1;

        panelMainMenu.SetActive(false);
        panelQuiz.SetActive(true);
        panelResult.SetActive(false);

        sliderProgress.maxValue = questions.Length;
        sliderProgress.value = 0;

        foreach (Button btn in answerButtons) {
            btn.interactable = true;
            btn.image.color = Color.white;
        }

        DisplayQuestion();
        UpdateScore();
    }

    public void SelectAnswer(int index) {
        if (answerSelected)
            return;

        selectedAnswerIndex = index;
        answerSelected = true;

        for (int i = 0; i < answerButtons.Length; i++) {
            if (i == index) {
                if (i == questions[currentQuestionIndex].correctAnswerIndex) {
                    answerButtons[i].image.color = Color.green;
                } else {
                    answerButtons[i].image.color = Color.red;
                }
            } else {
                answerButtons[i].image.color = Color.white;
            }

            answerButtons[i].interactable = false;
        }

        buttonNext.gameObject.SetActive(true);
    }

    public void NextQuestion() {
        if (selectedAnswerIndex == questions[currentQuestionIndex].correctAnswerIndex) {
            score += 10;
        }

        currentQuestionIndex++;
        selectedAnswerIndex = -1;
        answerSelected = false;
        buttonNext.gameObject.SetActive(false);

        if (currentQuestionIndex < questions.Length) {
            DisplayQuestion();

            foreach (Button btn in answerButtons) {
                btn.interactable = true;
                btn.image.color = Color.white;
            }
        } else {
            ShowResult();
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

        txtFinalScore.text = score.ToString();

        imageBronze.SetActive(false);
        imageSilver.SetActive(false);
        imageGold.SetActive(false);

        if (score >= 80) {
            txtAward.text = "🏅 Gold!";
            imageBronze.SetActive(true);
            imageSilver.SetActive(true);
            imageGold.SetActive(true);
        } else if (score >= 50) {
            txtAward.text = "🥈 Silver!";
            imageBronze.SetActive(true);
            imageSilver.SetActive(true);
        } else {
            txtAward.text = "🥉 Bronze!";
            imageBronze.SetActive(true);
        }

        // Putar sfx kalau sudah diassign
        if (sfxSource != null) {
            sfxSource.Play();
        }
    }

    public void Retry() {
        StartQuiz(questions);
    }

    public void BackToMenu() {
        panelResult.SetActive(false);
        panelQuiz.SetActive(false);
        panelMainMenu.SetActive(true);
    }

    public void Logout() {
        SceneManager.LoadScene("LoginScene");
    }
}
