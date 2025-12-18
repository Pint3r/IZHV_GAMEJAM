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

    public float minEnemySpeed = 2f;
    public float startEnemySpeed = 2f;
    public float increaseEnemySpeed = 0.01f;


    public int scoreStep = 5;
    public float decreaseRate = 0.01f;
    public float minSpawnRate = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        StartCoroutine(Spawner());
    }
    
    //spawns projectiles
    private IEnumerator Spawner()
    {
        float currentSpawnRate = spawnRate;
        while (canSpawn)
        {
            yield return new WaitForSeconds(currentSpawnRate);

            Vector3 spawnPosition = RandomOffScreenPosition();

            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[rand];

            GameObject newEnemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);

            //speed calculations
            int currentScore = ScoreManager.instance.GetScore();
            float currentSpeed = startEnemySpeed + (currentScore * increaseEnemySpeed);
            float randomSpeed = Random.Range(minEnemySpeed, currentSpeed);
            
            //spawnrate calculations
            int steps = currentScore / scoreStep;
            float newSpawnRate = spawnRate - (steps * decreaseRate);

            currentSpawnRate = Mathf.Max(newSpawnRate, minSpawnRate);

            targeting enemyScript = newEnemy.GetComponent<targeting>();

            enemyScript.speed = randomSpeed;


        }

    }

    //gives a random position for a projectile to spawn at
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
