using System;
using UnityEngine;

[Serializable]
public class BalanceData
{
    public float PLAYER_SPEED = 5f;
    public float JUMP_FORCE = 6.5f;
    public float GRAVITY = -9.81f;
    public int MAX_LIVES = 3;
    public int COINS_PER_PICKUP = 1;
}

/// <summary>
/// Simple static holder that loads Assets/Resources/Balance.json on game start.
/// Put this script anywhere under Assets (e.g., Assets/Game/Scripts).
/// </summary>
public static class Balance
{
    public static float PlayerSpeed { get; private set; } = 5f;
    public static float JumpForce   { get; private set; } = 6.5f;
    public static float Gravity     { get; private set; } = -9.81f;
    public static int   MaxLives    { get; private set; } = 3;
    public static int   CoinsPerPickup { get; private set; } = 1;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        var ta = Resources.Load<TextAsset>("Balance");
        if (ta == null)
        {
            Debug.LogWarning("Balance.json not found in Resources. Using defaults.");
            return;
        }

        try
        {
            var data = JsonUtility.FromJson<BalanceData>(ta.text);
            if (data != null)
            {
                PlayerSpeed = data.PLAYER_SPEED;
                JumpForce   = data.JUMP_FORCE;
                Gravity     = data.GRAVITY;
                MaxLives    = data.MAX_LIVES;
                CoinsPerPickup = data.COINS_PER_PICKUP;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to parse Balance.json: " + ex.Message);
        }
    }
}
