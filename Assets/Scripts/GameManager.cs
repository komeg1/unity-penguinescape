using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public enum GameState { Pause, Game, Win, Lose };
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
    [SerializeField] private Canvas winCanvas;
    [SerializeField] private Canvas loseCanvas;
    [SerializeField] private GameObject keysBar;
    private List<Image> keysList = new();

    private int score = 0;
    private int keys = 0;
    private int killedEnemies = 0;

    private bool pauseTime = false;
 
    public static GameManager instance;

    private void Awake()
    {
        CreateKeysBar();

        pauseCanvas.enabled = false;
        inGameCanvas.enabled = false;
        winCanvas.enabled = false;
        loseCanvas.enabled = false;

        instance = this;

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
    }
    public void Continue()
    {
        pauseTime = false;
        pauseCanvas.enabled = false;
        inGameCanvas.enabled = true;
        state = GameState.Game;
    }
    public void Pause()
    {
        pauseTime = true;
        inGameCanvas.enabled = false;
        pauseCanvas.enabled = true;
        state = GameState.Pause;
    }
    public void Lose()
    {
        inGameCanvas.enabled = false;
        loseCanvas.enabled = true;
        state = GameState.Lose;
    }
    public void Win()
    {
        inGameCanvas.enabled = false;
        winCanvas.enabled = true;
        state = GameState.Win;
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
}
