using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExampleSkinChange : MonoBehaviour
{
    int chosenSkin = 0;
    [SerializeField]AnimatorOverrideController[] overrideControllersArray;
    [SerializeField] private ChangeSkin overrider;
    void Start()
    {
        
    }

    public void OnRightArrowClick()
    {
        chosenSkin++;
        if (chosenSkin >= overrideControllersArray.Length)
            chosenSkin = 0;
        SetExampleSkin();
    }
    public void OnLeftArrowClick()
    {
        chosenSkin--;
        if (chosenSkin < 0)
            chosenSkin = overrideControllersArray.Length - 1;
        SetExampleSkin();
    }
    public void OnSelectButtonClick()
    {
        MainMenu.skinNumber = chosenSkin;
    }
    public void OnMainMenuButtonClick()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    // Update is called once per frame
    public void SetExampleSkin()
    {
        
        overrider.SetAnimations(chosenSkin);

    }

   
}
