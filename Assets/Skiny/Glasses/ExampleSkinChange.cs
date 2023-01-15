using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExampleSkinChange : MonoBehaviour
{
    struct SkinData {
        public int skinId;
        public bool isLocked;
        public int price;
    }

    

    int chosenSkin = 0;
    [SerializeField] AnimatorOverrideController[] overrideControllersArray;
    [SerializeField] private ChangeSkin overrider;
    private static SkinData[] skinsData;
    private static bool initDone = false;
    private Image tickImage;
    [SerializeField] private GameObject lockedText;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text priceText;
   
    void Start()
    {
        SetUI();

        //player = GameObject.FindWithTag("PlayerExample").GetComponent<SpriteRenderer>();
        if (initDone == false)
        {
            Debug.Log("init");
            skinsData = new SkinData[overrideControllersArray.Length];
            InitializeSkinsData();
            initDone = true;
        }
        

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
        selectButton.gameObject.SetActive(false);
        updateTick();
    }
    public void OnMainMenuButtonClick()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    // Update is called once per frame
    public void SetExampleSkin()
    {
        priceText.text = "" + skinsData[chosenSkin].price + "$";
        if (skinsData[chosenSkin].isLocked == true)
        {
            buyButton.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(false);
            lockedText.SetActive(true);
            
            
            //Debug.Log("here");
        }
        else
        {
            buyButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
            lockedText.SetActive(false);
            
        }
        updateTick();
        overrider.SetAnimations(chosenSkin);
        



    }

    public void updateTick()
    {
        if (MainMenu.skinNumber == chosenSkin)
        {
            tickImage.enabled = true;
            selectButton.gameObject.SetActive(false);
        }
        else
            tickImage.enabled = false;
    }

    public void InitializeSkinsData()
    {
        
        for (int i=1;i<overrideControllersArray.Length;i++)
        {
            
            skinsData[i].skinId = i;
            skinsData[i].isLocked = true;
        }

        //Default
        skinsData[0].skinId = 0;
        skinsData[0].price = 0;
        skinsData[0].isLocked = false;

        //Glasses
        skinsData[1].price = 500;

        
        //Pink
        skinsData[2].price = 1500;

        //Blue
        skinsData[3].price = 1500;

        //White
        skinsData[4].price = 2500;
    }

    public void SetUI()
    {
        lockedText = GameObject.Find("Locked");
        lockedText.SetActive(false);

        coinsText.text = ": " + MainMenu.coinsAmount;

        selectButton.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(false);
    }


    public void OnBuyButtonClick()
    {
        
        if(MainMenu.coinsAmount >= skinsData[chosenSkin].price)
        {
            skinsData[chosenSkin].isLocked = false;
            MainMenu.coinsAmount -= skinsData[chosenSkin].price;
            buyButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
            coinsText.text = ": " + MainMenu.coinsAmount;
            lockedText.SetActive(false);
        }
        else
        {
            Debug.Log("nie mozna kupic");
        }
    }

   
}
