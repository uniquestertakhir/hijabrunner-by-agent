using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    public Text coinsText;
    public Text livesText;
    public GameObject gameOverPanel;

    private int coins;
    private int lives;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        lives = Mathf.Max(1, Balance.Instance.MAX_LIVES);
        UpdateUI();
        if (gameOverPanel) gameOverPanel.SetActive(false);
    }

    public void CollectCoin()
    {
        coins += Mathf.Max(1, Balance.Instance.COINS_PER_PICKUP);
        UpdateUI();
    }

    public void HitObstacle()
    {
        lives -= 1;
        UpdateUI();
        if (lives <= 0)
        {
            GameOver();
        }
    }

    void UpdateUI()
    {
        if (coinsText) coinsText.text = $"Coins: {coins}";
        if (livesText) livesText.text = $"Lives: {lives}";
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        if (gameOverPanel)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
