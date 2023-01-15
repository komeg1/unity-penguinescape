using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class WinScreenManager : MonoBehaviour
{
    [SerializeField] TMP_Text winScoreText;
    [SerializeField] TMP_Text winHighScoreText;
    public static int score = 0;

    [SerializeField] ChangeSkin skinOverrider;
    private void Start()
    {
        skinOverrider.SetAnimations(MainMenu.skinNumber);
        int highScore = PlayerPrefs.GetInt("HighScore");
        if (highScore < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore = score;
        }
        winScoreText.text = "Score: " + score;
        winHighScoreText.text = "Best Score: " + highScore;
        Time.timeScale = 1.0f;
    }
    void ReloadScene()
    {
        SceneManager.LoadSceneAsync("Level1");
    }

    public void OnRestartButtonPressed()
    {
        ReloadScene();
    }
   
    public void OnMainMenuButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
