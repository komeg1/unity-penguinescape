using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExampleSkinChange : MonoBehaviour
{
    int chosenSkin = 0;
    [SerializeField] AnimatorOverrideController[] overrideControllersArray;
    [SerializeField] private ChangeSkin overrider;
    private Image tickImage;
    void Start()
    {
        tickImage = GameObject.Find("Tick").GetComponent<Image>() ;
        chosenSkin = MainMenu.skinNumber;
        SetExampleSkin();
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
        updateTick();
    }
    public void OnMainMenuButtonClick()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    // Update is called once per frame
    public void SetExampleSkin()
    {

        updateTick();
        overrider.SetAnimations(chosenSkin);

    }

    public void updateTick()
    {
        if (MainMenu.skinNumber == chosenSkin)
            tickImage.enabled = true;
        else
            tickImage.enabled = false;
    }

   
}
