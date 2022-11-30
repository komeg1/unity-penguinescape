using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int score = 0;
    private int coinScore = 10;

    private TMPro.TextMeshProUGUI text;
    [SerializeField] private GameObject textObject;
    private string scorePrefix = "Score: ";
    // Update is called once per frame
    void Start()
    {
        text = textObject.GetComponent<TMPro.TextMeshProUGUI>();
        text.SetText(scorePrefix + score);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            score += coinScore;
            other.gameObject.SetActive(false);
            text.SetText(scorePrefix + score);
        }
    }
}
