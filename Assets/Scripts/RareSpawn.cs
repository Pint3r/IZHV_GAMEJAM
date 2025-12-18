using UnityEngine;
using System.Collections;
public class RareSpawn : MonoBehaviour
{
    // Nastav si väčšie číslo ako pri nepriateľoch (napr. 10 alebo 15 sekúnd)
    [SerializeField] private float spawnRate = 10.0f;

    [SerializeField] private float spawnMarginY = 1f;
    [SerializeField] private float spawnMarginX = 1f;

    private Camera cam;
    [SerializeField] private bool canSpawn = true;

    [SerializeField] private GameObject[] bonusPrefabs;

    void Start()
    {
        cam = Camera.main;
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        while (canSpawn)
        {
            yield return new WaitForSeconds(spawnRate);

            Vector3 spawnPosition = RandomOffScreenPosition();
            int rand = Random.Range(0, bonusPrefabs.Length);
            GameObject bonusToSpawn = bonusPrefabs[rand];

            Instantiate(bonusToSpawn, spawnPosition, Quaternion.identity);

        }
    }

    private Vector3 RandomOffScreenPosition()
    {
        float screenHeight = 2f * cam.orthographicSize;
        float screenWidth = screenHeight * cam.aspect;

        float left = cam.transform.position.x - screenWidth / 2f - spawnMarginX;
        float right = cam.transform.position.x + screenWidth / 2f + spawnMarginX;
        float bottom = cam.transform.position.y - screenHeight / 2f - spawnMarginY;
        float top = cam.transform.position.y + screenHeight / 2f + spawnMarginY;

        int side = Random.Range(0, 4);

        float x = 0f;
        float y = 0f;

        switch (side)
        {
            case 0:
                x = left;
                y = Random.Range(bottom, top);
                break;
            case 1:
                x = right;
                y = Random.Range(bottom, top);
                break;
            case 2:
                x = Random.Range(left, right);
                y = bottom;
                break;
            case 3:
                x = Random.Range(left, right);
                y = top;
                break;
        }

        return new Vector3(x, y, 0);
    }
}