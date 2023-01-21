using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FinishPointScript : MonoBehaviour
{
    TMPro.TMP_Text info;
    string enterText = "[E] - Escape";
    string needMoreKeysText = "You need to find all your eggs!";
    private void Start()
    {
        info = GetComponentInChildren<TMP_Text>();
        info.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.instance.enoughKeys())
            info.SetText(enterText);
        else
            info.SetText(needMoreKeysText);

        if(collision.CompareTag("Player"))
            info.enabled = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            info.enabled = false;
    }
}
