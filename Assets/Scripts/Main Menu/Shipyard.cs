using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Shipyard : MonoBehaviour
{
    public GameObject buyButton, useButton, removeButton, skinPrice;

    
    public Text shipModelName, shipModelPrice, skinState;
    //public Transform rocket, part;
    public Animation notEnough;

    //public GameObject ShipPrefab;
    public Transform ship, shopSkins;

    public string activeSkin;

    List<GameObject> boatItems, shopItems;

    private int modelIndex = 0;

    void Start()
    {
        LoadItems();
        LoadActiveModel();
        LoadShopModel();
        LoadButton();
    }

    // When player press previous button.
    public void Previous()
    {
        // Check whether the active part is not the first.
        if (modelIndex > 0)
        {
            // Load previous part.
            modelIndex--;
            LoadShopModel();
            LoadButton();
        }
    }

    // When player press next button.
    public void Next()
    {
        // Check whether the active part is not the last.
        if (modelIndex < shopItems.Count - 1)
        {
            // Loads next part.
            modelIndex++;
            LoadShopModel();
            LoadButton();
        }
    }

    // When player press buy button.
    public void Buy()
    {
        // Take part script from the active part.
        Part part = shopItems[modelIndex].GetComponent<Part>();

        // Check if player has enough money to buy a part.
        if (Wallet.GetAmount() >= part.price)
        {
            // Save bought part value.
            PlayerPrefs.SetInt("SkinBought-" + shopItems[modelIndex].name, 1);
            // Loas add/remove button.
            LoadButton();
            // Subract part price from player wallet.
            Wallet.SetAmount(Wallet.GetAmount() - part.price);
        }
        else
        {
            //Play not enough money animation.
            notEnough.Play("Not-Enough-In");
        }
    }

    // When player press add button.
    public void Use()
    {
        // Save added part value.
        PlayerPrefs.SetString("ActiveSkin-", shopItems[modelIndex].name);
        // Load remove button. 
        LoadActiveModel();
        LoadButton();
        // Load rocket with added part.

        //ShipPrefab.GetComponent<SpriteRenderer>().sprite = shopItems[modelIndex].GetComponent<Image>().sprite;
       

    }

    // When player press remove button.
    public void Remove()
    {
        // Save removed part value.
        PlayerPrefs.SetString("ActiveSkin-", "Default");
        // Load add button.
        LoadActiveModel();
        LoadButton();
        // Load rocket with removed part.
        
    }

    // Loading parts at the start
    private void LoadItems()
    {
        // Load parts for the ship.
        boatItems = new List<GameObject>();
        activeSkin = PlayerPrefs.GetString("ActiveSkin-");

        foreach (Transform item in ship)
        {
            if (item.name == activeSkin) 
            {
                item.gameObject.SetActive(true);
            }
            else
            {
                item.gameObject.SetActive(false);
            }
                

        }

        // Load parts for the shop.
        shopItems = new List<GameObject>();

        foreach (Transform item in shopSkins)
        {
            shopItems.Add(item.gameObject);
        }
    }

    //Load ship parts.
    private void LoadActiveModel()
    {
        // Cycle between all ship skins.

        for (int i = 0; i < boatItems.Count; i++)
        {

            // Get value if rocket part is added.
            activeSkin = PlayerPrefs.GetString("ActiveSkin-");
            GameObject shopPart = boatItems[i];
            // Enable or disable shop model gameobject according to partAdded value.
           if (shopPart.name == activeSkin)
            {
                shopPart.SetActive(true);
            }
            else
            {
                shopPart.SetActive(false);
            }
        }
    }

    // Load only next/previous model, and sets active/false
    private void LoadShopModel()
    {
        // Cycle between all shop parts.
        for (int i = 0; i < shopItems.Count; i++)
        {
            // Load shop part gameobject.
            GameObject shopModel = shopItems[i];

            // Check the active part.
            if (i == modelIndex)
            {
                // Enable and change name for active part.
                shipModelName.text = shopModel.name;
                shopModel.SetActive(true);
            }
            else
            {
                // Otherwise disable part gameobject.
                shopModel.SetActive(false);
            }
        }
    }

    // Load button according to the part state.
    private void LoadButton()
    {   
        
        
        activeSkin = PlayerPrefs.GetString("ActiveSkin-");
        // Get value if part is bought.
        bool skinBought = PlayerPrefs.GetInt("SkinBought-" + shopItems[modelIndex].name, 0) == 1 ? true : false;
        if (skinBought)
        {
            skinPrice.SetActive(false);
            skinState.gameObject.SetActive(true);
            // Get value if part is added to the rocket.


            if (shopItems[modelIndex].name == activeSkin)
            {
                // Display remove button.
                DisplayButton(false, false, true);
                skinState.text = "Selected";

                if (shopItems[modelIndex].name == "Default")
                {
                    skinState.text = "Default";
                    DisplayButton(false, false, false);

                }
            }
            else
            {
                skinState.text = "Bought";
                // Display add button.
                DisplayButton(false, true, false);
            }
        }
        else
        {
            skinPrice.SetActive(true);
            skinState.gameObject.SetActive(false);
            // Display buy button with part price;
            DisplayButton(true, false, false);
            Part shopPart = shopItems[modelIndex].GetComponent<Part>();
            shipModelPrice.text = shopPart.price.ToString();
        }
    }

    // Changing between buttons.
    private void DisplayButton(bool buy, bool use, bool remove)
    {
        if (buy)
        {
            ResetButtonRect(buyButton);
        }
        buyButton.SetActive(buy);

        if (use)
        {
            ResetButtonRect(useButton);
        }
        useButton.SetActive(use);

        if (remove)
        {
            ResetButtonRect(removeButton);
        }
        removeButton.SetActive(remove);
    }

    // Each time button is loaded it's scale is reset to the default size.
    private void ResetButtonRect(GameObject button)
    {
        button.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
}
