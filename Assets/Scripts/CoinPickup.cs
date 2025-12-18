using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinAmount = 5;
    public float rotationSpeed = 180f;

    private void Update()
    {
        transform.Rotate(0,0,rotationSpeed*Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<PlayerHealth>())
        {
            
            CoinManager.instance.AddCoin(coinAmount);
            Destroy(gameObject);
        }
    }
}
