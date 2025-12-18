using UnityEngine;
using System.Collections; // Potrebné pre Coroutines (IEnumerator)

public class Deflect : MonoBehaviour
{
    private int coinsPerHit = 1;
    private SpriteRenderer shieldRenderer;
    private Color originalShieldColor;

    public AudioClip deflectSound;
    public AudioSource audioSource;
    void Start()
    {
        int incomeLevel = PlayerPrefs.GetInt("IncomeLevel", 0);
        coinsPerHit = 1 + incomeLevel;

        shieldRenderer = GetComponent<SpriteRenderer>();

        originalShieldColor = shieldRenderer.color;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var projectileTargeting = collision.GetComponent<targeting>();

        if (projectileTargeting != null)
        {
            if (CoinManager.instance != null) CoinManager.instance.AddCoin(coinsPerHit);
            if (ScoreManager.instance != null) ScoreManager.instance.AddScore();


            StopCoroutine("FlashShieldEffect");
            StartCoroutine("FlashShieldEffect");

            StartCoroutine(DeflectProjectileEffect(collision.gameObject, projectileTargeting));

            audioSource.PlayOneShot(deflectSound);
        }
    }

    IEnumerator FlashShieldEffect()
    {
        Color flashColor = originalShieldColor;
        flashColor.a = 0.6f;
        shieldRenderer.color = flashColor;

        yield return new WaitForSeconds(0.1f);

        shieldRenderer.color = originalShieldColor;
    }

    IEnumerator DeflectProjectileEffect(GameObject projectile, MonoBehaviour targetingScript)
    {
        targetingScript.enabled = false;

        Collider2D col = projectile.GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        Vector3 pushDirection = (projectile.transform.position - transform.position).normalized;
        float pushSpeed = 5f;

        SpriteRenderer projRenderer = projectile.GetComponent<SpriteRenderer>();
        float duration = 0.5f;
        float timer = 0f;
        Color startColor = Color.white;

        if (projRenderer != null) startColor = projRenderer.color;

        while (timer < duration)
        {
            if (projectile == null) yield break;

            timer += Time.deltaTime;

            projectile.transform.position += pushDirection * pushSpeed * Time.deltaTime;

            if (projRenderer != null)
            {
                float newAlpha = Mathf.Lerp(startColor.a, 0f, timer / duration);
                projRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            }

            yield return null; 
        }

        if (projectile != null)
        {
            Destroy(projectile);
        }
    }
}