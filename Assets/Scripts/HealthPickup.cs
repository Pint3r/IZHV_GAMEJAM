using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float rotationSpeed = 180f;

    private void Update()
    {
        transform.Rotate(0,0,rotationSpeed*Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.Heal(1);
            Destroy(gameObject);
        }
    }
}
