using UnityEngine;

public class targeting : MonoBehaviour
{
    public float speed;
    public GameObject target;
    public bool faceTarget = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) { return; }
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);

        if (faceTarget) {
            Vector2 direction = (target.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

    }
}
