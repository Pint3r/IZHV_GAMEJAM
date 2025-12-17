using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text spasmPriceText;
    public TMP_Text healthPriceText;

    public int baseSpasmCost = 10;
    public int baseHealthCost = 25;

    void Start()
    {
        UpdateShopUI();
    }

    public void BuySpasmUpgrade()
    {
        int currentLevel = PlayerPrefs.GetInt("SpasmLevel", 0);
        int cost = baseSpasmCost * (currentLevel + 1);

        if (CoinManager.instance.totalCoins >= cost)
        {
            CoinManager.instance.AddCoin(-cost);
            PlayerPrefs.SetInt("SpasmLevel", currentLevel + 1); 
            PlayerPrefs.Save();

            UpdateShopUI();
            Debug.Log("Kupene! Novy Spasm Level: " + (currentLevel + 1));
        }
        else
        {
            Debug.Log("Nemas dost penazi!");
        }
    }

    public void BuyHealthUpgrade()
    {
        int currentLevel = PlayerPrefs.GetInt("HealthLevel", 0);
        int cost = baseHealthCost * (currentLevel + 1);

        if (CoinManager.instance.totalCoins >= cost)
        {
            CoinManager.instance.AddCoin(-cost);
            PlayerPrefs.SetInt("HealthLevel", currentLevel + 1);
            PlayerPrefs.Save();

            UpdateShopUI();
            Debug.Log("Kupene! Novy Health Level: " + (currentLevel + 1));
        }
        else
        {
            Debug.Log("Nemas dost penazi!");
        }
    }

    void UpdateShopUI()
    {
        int spasmLvl = PlayerPrefs.GetInt("SpasmLevel", 0);
        int spasmCost = baseSpasmCost * (spasmLvl + 1);
        if (spasmPriceText != null)
            spasmPriceText.text = "Cena: " + spasmCost;

        int healthLvl = PlayerPrefs.GetInt("HealthLevel", 0);
        int healthCost = baseHealthCost * (healthLvl + 1);
        if (healthPriceText != null)
            healthPriceText.text = "Cena: " + healthCost;
    }
}