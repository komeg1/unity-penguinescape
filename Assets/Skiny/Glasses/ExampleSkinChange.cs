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
    [SerializeField] private GameObject lockedText;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private GameObject selectObject;
    [SerializeField] private GameObject buyObject;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private GameObject cantBuyObject;
   
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
        selectObject.SetActive(false);
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
            buyObject.gameObject.SetActive(true);
            selectObject.gameObject.SetActive(false);
            lockedText.SetActive(true);
            
            
            //Debug.Log("here");
        }
        else
        {
            buyObject.gameObject.SetActive(false);
            Debug.Log("Selected: " + MainMenu.skinNumber + " selected in shop: " + chosenSkin);
            if (MainMenu.skinNumber != chosenSkin)
                selectObject.gameObject.SetActive(true);
            else
                selectObject.gameObject.SetActive(false);

            lockedText.SetActive(false);
            
        }
        overrider.SetAnimations(chosenSkin);
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
        skinsData[1].price = 1000;

        
        //Pink
        skinsData[2].price = 1500;

        //Blue
        skinsData[3].price = 1500;

        //Yellow
        skinsData[4].price = 1500;

        //White
        skinsData[5].price = 2500;

        //Black
        skinsData[6].price = 2500;

        //Ninja
        skinsData[7].price = 4000;

        //Rainbow
        skinsData[8].price = 9999;

    }

    public void SetUI()
    {
        cantBuyObject.SetActive(false);
        lockedText = GameObject.Find("Locked");
        lockedText.SetActive(false);

        coinsText.text = ": " + MainMenu.coinsAmount;

        selectObject.gameObject.SetActive(false);
        buyObject.gameObject.SetActive(false);
    }


    public void OnBuyButtonClick()
    {
        
        if(MainMenu.coinsAmount >= skinsData[chosenSkin].price)
        {
            skinsData[chosenSkin].isLocked = false;
            MainMenu.coinsAmount -= skinsData[chosenSkin].price;
            buyObject.gameObject.SetActive(false);
            selectObject.gameObject.SetActive(true);
            coinsText.text = ": " + MainMenu.coinsAmount;
            lockedText.SetActive(false);
        }
        else
        {
            cantBuyObject.SetActive(true);
            StartCoroutine(Delay());
            
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        cantBuyObject.SetActive(false);
    }

   
}
