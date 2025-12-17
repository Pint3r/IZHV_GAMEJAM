using System.Collections;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    private float spawnMarginY = 0f;
    private float spawnMarginX = 0f;
    private Camera cam;
    [SerializeField] private float spawnRate = 1.0f;

    [SerializeField] private bool canSpawn = true;

    [SerializeField] private GameObject[] enemyPrefabs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        StartCoroutine(Spawner());
    }
    
    //spawns projectiles
    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn)
        {
            yield return wait;

            Vector3 spawnPosition = RandomOffScreenPosition();

            int rand = Random.Range(0,enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[rand];

            Instantiate(enemyToSpawn , spawnPosition, Quaternion.identity);
        }

    }

    //gives a random position for a projectile to spawn at
    private Vector3 RandomOffScreenPosition()
    {
        float screenHeight = 2f * cam.orthographicSize;
        float screenWidth = screenHeight * cam.aspect;

        float left = cam.transform.position.x - screenWidth / 2f - spawnMarginX;
        float right = cam.transform.position.x + screenWidth / 2f + spawnMarginX;
        float bottom = cam.transform.position.x - screenHeight / 2f - spawnMarginY;
        float top = cam.transform.position.x + screenHeight / 2f + spawnMarginY;

        int side = Random.Range(0, 4);

        float x = 0f;
        float y = 0f;

        switch (side)
        {
            case 0:
                x = left;
                y = Random.Range(bottom,top);
                break;
            case 1:
                x = right;
                y = Random.Range(bottom,top);
                break;
            case 2:
                x= Random.Range(left,right);
                y = bottom;
                break;
            case 3:
                x = Random.Range(left,right);
                y = top;
                break;
        }

        return new Vector3(x, y, 0);
    }
}
