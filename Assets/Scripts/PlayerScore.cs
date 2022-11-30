using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int score = 0;
    private int coinScore = 10;
    private int winScore = 100;

    private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private GameObject textObject;
    [SerializeField] private GameObject winTextObject;
    [SerializeField] private GameObject finishScoreObject;

    private string scorePrefix = "Score: ";
    private string finishScorePrefix = "Score needed: ";
    // Update is called once per frame
    void Start()
    {
        scoreText = textObject.GetComponent<TMPro.TextMeshProUGUI>();
        scoreText.SetText(scorePrefix + score);
        finishScoreObject.GetComponent<TMPro.TextMeshProUGUI>().SetText(finishScorePrefix + winScore);
        winTextObject.active = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            score += coinScore;
            other.gameObject.SetActive(false);
            scoreText.SetText(scorePrefix + score);
        }
        else if(other.CompareTag("Finish Line"))
        {
            if(score >= winScore)
            {
                winTextObject.active = true;
            }
        }
    }
}
