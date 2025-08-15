using UnityEngine;

[System.Serializable]
public class BalanceData {
    public float PLAYER_SPEED = 7.5f;
    public float JUMP_FORCE = 7.0f;
    public float GRAVITY = -9.6f;
    public int MAX_LIVES = 5;
    public int COINS_PER_PICKUP = 2;
}

public static class Balance {
    private static BalanceData _d;
    private static BalanceData D {
        get {
            if (_d == null) {
                var ta = Resources.Load<TextAsset>("Balance");
                _d = ta ? JsonUtility.FromJson<BalanceData>(ta.text) : new BalanceData();
            }
            return _d;
        }
    }

    public static float PlayerSpeed => D.PLAYER_SPEED;
    public static float JumpForce => D.JUMP_FORCE;
    public static float Gravity => D.GRAVITY;
    public static int MaxLives => D.MAX_LIVES;
    public static int CoinsPerPickup => D.COINS_PER_PICKUP;
}
