using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [Header("UI Settings")]
    public TMP_Text coinText;

    public int totalCoins = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        totalCoins = PlayerPrefs.GetInt("SavedCoins", 0);

        UpdateUI();
    }

    public void AddCoin(int amount)
    {
        totalCoins += amount;

        PlayerPrefs.SetInt("SavedCoins", totalCoins);
        PlayerPrefs.Save();

        UpdateUI();
    }

    void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = totalCoins.ToString();
        }
    }

}