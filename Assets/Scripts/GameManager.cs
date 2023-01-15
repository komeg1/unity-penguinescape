using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    public enum GameState { Pause, Game, Win, Lose, Options };
    public int cherryScore = 10;
    public int maxKeys = 3;
    private float time = 0f;
    public GameState state = GameState.Pause;

    

    [SerializeField] private Canvas inGameCanvas;
    [SerializeField] private Texture2D keyIcon;
    [SerializeField] private int keyIconPixelsPerUnit;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text killedEnemiesText;
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private Canvas optionsCanvas;
    [SerializeField] private Canvas winCanvas;
    [SerializeField] private Canvas loseCanvas;
    [SerializeField] private GameObject keysBar;
    [SerializeField] private TMP_Text qualityText;
    [SerializeField] private TMP_Text winScoreText;
    [SerializeField] private TMP_Text winHighScoreText;

    [SerializeField] private GameObject water;
    [SerializeField] private GameObject player;

    [SerializeField] private float waterRiseDelay = 5f;
    [SerializeField] private float waterRiseSpeed = 0f;
    [SerializeField] private float waterMaxRiseSpeed = 1f;
    [SerializeField] private float waterRiseAcceleration = 0.01f;


    
    [SerializeField] private ChangeSkin overrider;

    private List<Image> keysList = new();

    public int score = 0;
    private int keys = 0;
    private int killedEnemies = 0;

    private bool pauseTime = false;
 
    public static GameManager instance;

    private void Awake()
    {
        CreateKeysBar();

        Application.targetFrameRate = 60;

        pauseCanvas.enabled = false;
        inGameCanvas.enabled = false;
        optionsCanvas.enabled = false;
        winCanvas.enabled = false;
        loseCanvas.enabled = false;

        instance = this;
        SetSkin();
        SetQualityText();
        SetVolume(0.35f);
        Continue();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (state == GameState.Game)
                Pause();
            else if (state == GameState.Pause)
                Continue();
        }
        if(pauseTime == false )
            time += Time.deltaTime;
        timeText.SetText(string.Format("{0:00}:{1:00}", (int)time / 60, (int)time % 60));

        if(time > waterRiseDelay)
        {
            water.transform.position += new Vector3(0, waterRiseSpeed * Time.deltaTime);
            if(waterRiseSpeed < waterMaxRiseSpeed)
            {
                waterRiseSpeed += waterRiseAcceleration * Time.deltaTime;
            }
        }
    }
    public void Continue()
    {
        
        pauseTime = false;
        pauseCanvas.enabled = false;
        optionsCanvas.enabled = false;
        inGameCanvas.enabled = true;
        GUI.FocusControl(null);
        state = GameState.Game;
        Time.timeScale = 1;
    }
    public void Pause()
    {
        pauseTime = true;
        inGameCanvas.enabled = false;
        pauseCanvas.enabled = true;
        state = GameState.Pause;
        Time.timeScale = 0;
    }
    public void Lose()
    {
        inGameCanvas.enabled = false;
        loseCanvas.enabled = true;
        state = GameState.Lose;
        Time.timeScale = 0;
    }
    public void Win()
    {
        inGameCanvas.enabled = false;
        winCanvas.enabled = true;
        state = GameState.Win;
        Time.timeScale = 0;
        MainMenu.coinsAmount += score;
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "Level1")
        {
            int highScore = PlayerPrefs.GetInt("HighScore");
            if (highScore < score)
            {
                PlayerPrefs.SetInt("HighScore", score);
                highScore = score;
            }

            winScoreText.text = "Score: " + score;
            winHighScoreText.text = "Best Score: " + highScore;
        }
    }
    public void Options()
    {
        inGameCanvas.enabled = false;
        optionsCanvas.enabled = true;
        state = GameState.Options;
        Time.timeScale = 0;
    }

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

    public void AddKey()
    {
        keysList[keys].color = Color.white;
        keys += 1;
    }

    public void RemoveKey()
    {
        keysList[keys].color = Color.grey;
        keys -= 1;
    }

    public bool enoughKeys()
    {
        return keys >= maxKeys;
    }

    void ReloadScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void IncreaseKilledEnemies()
    {
        killedEnemies++;
        killedEnemiesText.SetText(killedEnemies.ToString());
    }

    void CreateKeysBar()
    {
        for (int i = 0; i < maxKeys; i++)
        {
            GameObject newKeyIcon = new GameObject();
            RectTransform keyIconTransform = newKeyIcon.AddComponent<RectTransform>();
            keyIconTransform.sizeDelta = new Vector2(50, 50);

            Image keyIconImage = newKeyIcon.AddComponent<Image>();
            keysList.Add(keyIconImage);

            Sprite healthIconSprite = Sprite.Create(keyIcon, new Rect(0f, 0f, keyIcon.width, keyIcon.height), new Vector2(0f, 0f), keyIconPixelsPerUnit);
            keyIconImage.sprite = healthIconSprite;
            keyIconImage.color = Color.grey;

            newKeyIcon.transform.SetParent(keysBar.transform);
            newKeyIcon.transform.localPosition = new Vector2(keyIconTransform.sizeDelta.x / 2 + i * keyIconTransform.sizeDelta.x, 0f);
        }
    }

    public void OnResumeButtonPressed()
    {
        Continue();
    }
    public void OnRestartButtonPressed()
    {
        ReloadScene();
    }
    public void OnOptionsButtonPressed()
    {
        Options();
    }
    public void OnMainMenuButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnPlusButtonPressed()
    {
        QualitySettings.IncreaseLevel();
        SetQualityText();
    }
    public void OnMinusButtonPressed()
    {
        QualitySettings.DecreaseLevel();
        SetQualityText();
    }
    public void SetQualityText()
    {
        qualityText.text="Quality: " + QualitySettings.names[QualitySettings.GetQualityLevel()];
    }

    public void SetVolume( float volume )
    {
        AudioListener.volume = volume;
    }

    public void SetSkin()
    {
      
        overrider.SetAnimations(MainMenu.skinNumber);

    }
}
