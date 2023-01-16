using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static int skinNumber = 0;
    [SerializeField] ChangeSkin exampleOverrider;
    [SerializeField] GameObject tutorialPanel;
    public static int coinsAmount=900;
    

    void Start()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1f;
        exampleOverrider.SetAnimations(skinNumber);
        Debug.Log("Coins: " + coinsAmount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnLevel1ButtonPressed()
    {
        SceneManager.LoadSceneAsync("Level1");
    }

    public void OnCustomizationButtonPressed()
    {
        SceneManager.LoadSceneAsync("Customization");
    }

    public void OnTutorialButtonPressed()
    {
        tutorialPanel.SetActive(true);
    }

    public void OnTutorialExitButton()
    {
        tutorialPanel.SetActive(false);
    }

    public void OnExitToDesktopButtonPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

}
