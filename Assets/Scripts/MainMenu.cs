using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static int skinNumber = 0;
    [SerializeField] ChangeSkin exampleOverrider;
    

    void Start()
    {
        exampleOverrider.SetAnimations(skinNumber);

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
    public void OnExitToDesktopButtonPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

}
