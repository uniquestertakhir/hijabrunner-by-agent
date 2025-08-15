using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text distText;
    [SerializeField] private TMP_Text stateText;

    public void Init(int lives, int coins, float dist, GameManager.State s)
    {
        SetLives(lives);
        SetCoins(coins);
        SetDistance(dist);
        SetState(s);
    }

    public void SetLives(int v)
    {
        if (livesText != null)
            livesText.text = $"Lives: {v}";
    }

    public void SetCoins(int v)
    {
        if (coinsText != null)
            coinsText.text = $"Coins: {v}";
    }

    public void SetDistance(float v)
    {
        if (distText != null)
            distText.text = $"{v:F0} m";
    }

    public void SetState(GameManager.State s)
    {
        if (stateText != null)
            stateText.text = s.ToString();
    }
}
