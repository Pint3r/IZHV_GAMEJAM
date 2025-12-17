using UnityEngine;
using UnityEngine.UI;
public class GameOverScript : MonoBehaviour
{
    public GameObject gameOverScreen;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += GameIsOver;
    }
    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= GameIsOver;
    }
    void GameIsOver()
    {
        if (gameOverScreen != null) {
            
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void Setup()
    {
        gameObject.SetActive(true);
    }
}
