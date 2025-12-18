using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameOverScript : MonoBehaviour
{
    public TMP_Text highScoreText;
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

            ScoreManager.instance.CheckHighScore();

            int bestScore = ScoreManager.instance.GetHighScore();
            int currentScore = ScoreManager.instance.GetScore();
            highScoreText.text = "SCORE: " + currentScore + "\n HIGHSCORE: " + bestScore;
        }
        Time.timeScale = 0f;
    }
    public void Setup()
    {
        gameObject.SetActive(true);
    }
}
