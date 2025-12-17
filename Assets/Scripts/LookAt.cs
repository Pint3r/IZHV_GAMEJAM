using UnityEngine;

public class LookAt : MonoBehaviour
{
    private Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = (Vector2)(cam.ScreenToWorldPoint(Input.mousePosition));
        float angleRad = Mathf.Atan2(mousePosition.y - transform.position.y , mousePosition.x - transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad - 90;

        transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);

    }
}
