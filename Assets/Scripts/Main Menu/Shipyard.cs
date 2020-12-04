using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Shipyard : MonoBehaviour
{
    public GameObject buyButton, useButton, removeButton;

    
    public Text shipModel, shipModelPrice;
    //public Transform rocket, part;
    public Animation notEnough;

    public GameObject ShipPrefab;
    public Transform ship, shopModel;
    

    List<GameObject> boatItems, shopItems;

    private int partIndex = 0;

    void Start()
    {
        LoadItems();
        LoadRocket();
        LoadPart();
        LoadButton();
    }

    // When player press previous button.
    public void Previous()
    {
        // Check whether the active part is not the first.
        if (partIndex > 0)
        {
            // Load previous part.
            partIndex--;
            LoadPart();
            LoadButton();
        }
    }

    // When player press next button.
    public void Next()
    {
        // Check whether the active part is not the last.
        if (partIndex < shopItems.Count - 1)
        {
            // Loads next part.
            partIndex++;
            LoadPart();
            LoadButton();
        }
    }

    // When player press buy button.
    public void Buy()
    {
        // Take part script from the active part.
        Part part = shopItems[partIndex].GetComponent<Part>();

        // Check if player has enough money to buy a part.
        if (Wallet.GetAmount() >= part.price)
        {
            // Save bought part value.
            PlayerPrefs.SetInt("PartBought-" + shopItems[partIndex].name, 1);
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
    public void Add()
    {
        // Save added part value.
        PlayerPrefs.SetInt("PartAdded-" + shopItems[partIndex].name, 1);
        // Load remove button.
        LoadButton();
        // Load rocket with added part.
        ShipPrefab.GetComponent<SpriteRenderer>().sprite = shopItems[partIndex].GetComponent<Image>().sprite;
        LoadRocket();

    }

    // When player press remove button.
    //public void Remove()
    //{
    //    // Save removed part value.
    //    PlayerPrefs.SetInt("PartAdded-" + shopItems[partIndex].name, 0);
    //    // Load add button.
    //    LoadButton();
    //    // Load rocket with removed part.
    //    LoadRocket();
    //}

    // Loading parts
    private void LoadItems()
    {
        // Load parts for the rocket.
        boatItems = new List<GameObject>();

        foreach (Transform item in ship)
        {
            if (item.name != "Base")
                boatItems.Add(item.gameObject);

        }

        // Load parts for the shop.
        shopItems = new List<GameObject>();

        foreach (Transform item in shopModel)
        {
            shopItems.Add(item.gameObject);
        }
    }

    //Load rocket parts.
    private void LoadRocket()
    {
        // Cycle between all rocket parts.

        for (int i = 0; i < boatItems.Count; i++)
        {
            // Get value if rocket part is added.
            bool partAdded = PlayerPrefs.GetInt("PartAdded-" + shopItems[i].name, 0) == 1 ? true : false;
            GameObject shopPart = boatItems[i];
            // Enable or disable rocket part gameobject according to partAdded value.
            shopPart.SetActive(partAdded);
        }
    }

    // Loading shop parts.
    private void LoadPart()
    {
        // Cycle between all shop parts.
        for (int i = 0; i < shopItems.Count; i++)
        {
            // Load shop part gameobject.
            GameObject shopPart = shopItems[i];

            // Check the active part.
            if (i == partIndex)
            {
                // Enable and change name for active part.
                shipModel.text = shopPart.name;
                shopPart.SetActive(true);
            }
            else
            {
                // Otherwise disable part gameobject.
                shopPart.SetActive(false);
            }
        }
    }

    // Load button according to the part state.
    private void LoadButton()
    {
        // Get value if part is bought.
        bool partBought = PlayerPrefs.GetInt("PartBought-" + shopItems[partIndex].name, 0) == 1 ? true : false;
        if (partBought)
        {
            // Get value if part is added to the rocket.
            bool partAdded = PlayerPrefs.GetInt("PartAdded-" + shopItems[partIndex].name, 0) == 1 ? true : false;
            if (partAdded)
            {
                // Display remove button.
                DisplayButton(false, true/* true*/);
            }
            else
            {
                // Display add button.
                DisplayButton(false, true /*false*/);
            }
        }
        else
        {
            // Display buy button with part price;
            DisplayButton(true, false /*false*/);
            Part shopPart = shopItems[partIndex].GetComponent<Part>();
            shipModelPrice.text = shopPart.price.ToString();
        }
    }

    // Changing between buttons.
    private void DisplayButton(bool buy, bool add/*, bool remove*/)
    {
        if (buy)
        {
            ResetButtonRect(buyButton);
        }
        buyButton.SetActive(buy);

        if (add)
        {
            ResetButtonRect(useButton);
        }
        useButton.SetActive(add);

        //if (remove)
        //{
        //    ResetButtonRect(removeButton);
        //}
        //removeButton.SetActive(remove);
    }

    // Each time button is loaded it's scale is reset to the default size.
    private void ResetButtonRect(GameObject button)
    {
        button.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
}
