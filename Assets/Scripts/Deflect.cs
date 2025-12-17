using UnityEngine;


public class Deflect : MonoBehaviour
{
    private int coinsPerHit = 1;
    private void Start()
    {
        int incomeLevel = PlayerPrefs.GetInt("IncomeLevel", 0);
        coinsPerHit = 1 + incomeLevel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<targeting>())
        {
            Destroy(collision.gameObject);
            CoinManager.instance.AddCoin(1);
        }
    }
}
