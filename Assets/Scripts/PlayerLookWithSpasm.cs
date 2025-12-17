using UnityEngine;
using UnityEngine.UI;

public class PlayerLookWithSpasm : MonoBehaviour
{
    private Camera cam;

    public Slider spasmSlider;

    public Image spasmFillImage;
    public float maxSpasmMeter = 100f;
    public float currentSpasmValue = 0f;

    public float fillMultiplier = 0.5f;
    public float decayRate = 20f;
    public float spasmDuration = 2f;

    private bool isSpasming = false;
    private float spasmTimer = 0f;
    private float currentAngleDeg;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        cam = Camera.main;

        spriteRenderer = GetComponent<SpriteRenderer>();

        currentAngleDeg = transform.rotation.eulerAngles.z;

        int spasmLevel = PlayerPrefs.GetInt("SpasmLevel", 0);
        fillMultiplier = 0.5f - (spasmLevel * 0.05f);

        if (spasmSlider != null)
        {
            spasmSlider.maxValue = maxSpasmMeter;
            spasmSlider.value = 0f;
        }


    }

    void Update()
    {
        if (isSpasming)
        {
            HandleSpasmState();
        }
        else
        {
            HandleRotation();
        }

        UpdateVisuals();
    }

    void HandleRotation()
    {
        Vector3 mousePosition = (Vector2)(cam.ScreenToWorldPoint(Input.mousePosition));
        float angleRad = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x);
        float targetAngleDeg = (180 / Mathf.PI) * angleRad - 90;

        float rotationDiff = Mathf.Abs(Mathf.DeltaAngle(currentAngleDeg, targetAngleDeg));

        if (rotationDiff > 0.1f)
        {
            currentSpasmValue += rotationDiff * fillMultiplier;
        }

        currentSpasmValue -= decayRate * Time.deltaTime;
        currentSpasmValue = Mathf.Clamp(currentSpasmValue, 0f, maxSpasmMeter);

        if (currentSpasmValue >= maxSpasmMeter)
        {
            TriggerSpasm();
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, targetAngleDeg);
            currentAngleDeg = targetAngleDeg;
        }
    }

    void TriggerSpasm()
    {
        isSpasming = true;
        spasmTimer = spasmDuration;
        if (spriteRenderer != null) spriteRenderer.color = Color.gray;
    }

    void HandleSpasmState()
    {
        spasmTimer -= Time.deltaTime;
        float shakeAmount = 5f;
        float randomZ = currentAngleDeg + Random.Range(-shakeAmount, shakeAmount);
        transform.rotation = Quaternion.Euler(0f, 0f, randomZ);

        if (spasmTimer <= 0)
        {
            isSpasming = false;
            currentSpasmValue = 0f;
            if (spriteRenderer != null) spriteRenderer.color = Color.white;
        }
    }

    void UpdateVisuals()
    {
        float t = currentSpasmValue / maxSpasmMeter;

        if (spasmSlider != null)
        {
            spasmSlider.value = currentSpasmValue;
        }

        if (spasmFillImage != null)
        {
            spasmFillImage.color = Color.Lerp(Color.green, Color.red, t);
        }

        if (spriteRenderer != null && !isSpasming)
        {
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, t);
        }
    }
}