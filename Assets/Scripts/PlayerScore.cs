using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int score = 0;
    private int keys = 0;
    private int coinScore = 10;
    [SerializeField] private int winKeys = 3;

    private TMPro.TextMeshProUGUI scoreText;
    private TMPro.TextMeshProUGUI keysText;
    [SerializeField] private GameObject scoreCounterTextObject;
    [SerializeField] private GameObject keysCounterTextObject;
    [SerializeField] private GameObject winScreenTextObject;
    [SerializeField] private GameObject keysNeededToWinTextObject;

    private string scorePrefix = "Score: ";
    private string keysPrefix = "Keys: ";
    private string keysNeededToWinTextPrefix = "Keys needed: ";
    // Update is called once per frame
    void Start()
    {
        scoreText = scoreCounterTextObject.GetComponent<TMPro.TextMeshProUGUI>();
        keysText = keysCounterTextObject.GetComponent<TMPro.TextMeshProUGUI>();

        scoreText.SetText(scorePrefix + score);
        keysText.SetText(keysPrefix + score);
        keysNeededToWinTextObject.GetComponent<TMPro.TextMeshProUGUI>().SetText(keysNeededToWinTextPrefix + winKeys);
        winScreenTextObject.active = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            other.GetComponent<CoinScript>().pickedUp();
            score += coinScore;
            scoreText.SetText(scorePrefix + score);
        }else if (other.CompareTag("Key"))
        {
            other.GetComponent<CoinScript>().pickedUp();
            keys += 1;
            keysText.SetText(keysPrefix + keys);
        }
        else if(other.CompareTag("Finish Line"))
        {
            if(keys >= winKeys)
            {
                winScreenTextObject.active = true;
            }
        }
        else if (other.CompareTag("Life"))
        {
            PlayerLife pl = GetComponent<PlayerLife>();
            pl.health += 1;
            pl.lifeCounterText.SetText(pl.healthCounterPrefix + pl.health);
            other.GetComponent<CoinScript>().pickedUp();
        }
    }
}
