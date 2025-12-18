using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Pre načítanie Win scény
using UnityEngine.UI;
public class ShopManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text spasmPriceText;
    public TMP_Text healthPriceText;
    public TMP_Text incomePriceText;

    public Button cureButton;
    public TMP_Text cureButtonText;

    private const int MAX_SPASM_LEVEL = 5;
    private const int MAX_HEALTH_LEVEL = 6;
    private const int MAX_INCOME_LEVEL = 5;

    public int baseSpasmCost = 10;
    public int baseHealthCost = 25;
    public int baseIncomeCost = 50;
    public int cureCost = 1500;

    void Start()
    {
        UpdateShopUI();
    }

    public void BuyIncomeUpgrade()
    {
        int currentLevel = PlayerPrefs.GetInt("IncomeLevel", 0);

        if (currentLevel >= MAX_INCOME_LEVEL) { return; }

        int cost = baseIncomeCost * ((currentLevel+1) *2);

        if (CoinManager.instance.totalCoins >= cost)
        {
            CoinManager.instance.AddCoin(-cost);
            PlayerPrefs.SetInt("IncomeLevel", currentLevel + 1);
            PlayerPrefs.Save();

            UpdateShopUI();
        }
    }

    bool CanBuyCure()
    {
        int level = PlayerPrefs.GetInt("SpasmLevel", 0);
        return (level >= MAX_SPASM_LEVEL);
    }
    public void BuyCure()
    {
        if (!CanBuyCure()) return;

        if (CoinManager.instance.totalCoins >= cureCost) {
            CoinManager.instance.AddCoin(-cureCost);
            PlayerPrefs.SetInt("HasCure", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("WIN"); 
        }

    }

    public void BuySpasmUpgrade()
    {
        int currentLevel = PlayerPrefs.GetInt("SpasmLevel", 0);

        if (currentLevel >= MAX_SPASM_LEVEL) { return; }

        int cost = baseSpasmCost * (currentLevel + 1);

        if (CoinManager.instance.totalCoins >= cost)
        {
            CoinManager.instance.AddCoin(-cost);
            PlayerPrefs.SetInt("SpasmLevel", currentLevel + 1); 
            PlayerPrefs.Save();

            UpdateShopUI();
        }

    }

    public void BuyHealthUpgrade()
    {
        int currentLevel = PlayerPrefs.GetInt("HealthLevel", 0);

        if (currentLevel >= MAX_HEALTH_LEVEL) { return; }

        int cost = baseHealthCost * (currentLevel + 1);

        if (CoinManager.instance.totalCoins >= cost)
        {
            CoinManager.instance.AddCoin(-cost);
            PlayerPrefs.SetInt("HealthLevel", currentLevel + 1);
            PlayerPrefs.Save();

            UpdateShopUI();
        }

    }

    void UpdateShopUI()
    {
        int spasmLvl = PlayerPrefs.GetInt("SpasmLevel", 0);
        int spasmCost = baseSpasmCost * (spasmLvl + 1);

        if (spasmLvl >= MAX_SPASM_LEVEL)
        {
            spasmPriceText.text = "MAX";
        }
        else
        {
            spasmPriceText.text = "Price: " + spasmCost;
        }

        int healthLvl = PlayerPrefs.GetInt("HealthLevel", 0);
        int healthCost = baseHealthCost * (healthLvl + 1);

        if (healthLvl >= MAX_HEALTH_LEVEL)
        {
            healthPriceText.text = "MAX";
        }
        else
        {
            healthPriceText.text = "Price: " + healthCost;
        }

        int incomeLvl = PlayerPrefs.GetInt("IncomeLevel", 0);
        int incomeCost = baseIncomeCost * ((incomeLvl+1) * 2);

        if (incomeLvl >= MAX_INCOME_LEVEL)
        {   
            incomePriceText.text = "MAX";
        }
        else
        {
            incomePriceText.text = "Price: " + incomeCost;
        }

        if (CanBuyCure())
        {
            cureButton.interactable = true;
            cureButtonText.text = "BUY CURE: " + cureCost;
        }
        else
        {
            cureButton.interactable = false;
            cureButtonText.text = "Locked Dizzy LVL:" + spasmLvl + "/5";  
        }

    }
}