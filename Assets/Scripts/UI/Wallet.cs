using UnityEngine;
using UnityEngine.UI;


public class Wallet : MonoBehaviour
{
    private static int amount;
    private static Text walletText;

    void Start()
    {
        // Testausta varten oletusrahamäärä tässä 10000, eikä nolla:
        amount = PlayerPrefs.GetInt("WalletAmount", 0);

        walletText = this.GetComponent<Text>();
        DisplayAmount();
    }

    // Get current player wallet amount.
    public static int GetAmount()
    {
        return amount;
    }

    // Set player amount to custom value.
    public static void SetAmount(int amountToSet)
    {
        amount = amountToSet;
        DisplayAmount();
        PlayerPrefs.SetInt("WalletAmount", amount);
    }

    // Display player amount to the screen.
    private static void DisplayAmount()
    {
        walletText.text = amount.ToString();
    }
}
