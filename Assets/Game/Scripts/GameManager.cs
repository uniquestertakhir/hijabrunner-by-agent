using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public enum State { Menu, Playing, Paused, GameOver }
    public State CurrentState { get; private set; } = State.Menu;

    public int Lives { get; private set; }
    public int Coins { get; private set; }
    public float Distance { get; private set; }

    [SerializeField] private HUDController hud;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        ResetRun();
    }

    private void Update() {
        if (CurrentState == State.Playing) {
            Distance += Balance.PlayerSpeed * Time.deltaTime;
            hud?.SetDistance(Distance);
        }
        if (CurrentState == State.Menu && Input.anyKeyDown) StartRun();
        if (CurrentState == State.GameOver && Input.anyKeyDown) ResetRun();
    }

    public void StartRun() {
        CurrentState = State.Playing;
        hud?.SetState(CurrentState);
    }

    public void ResetRun() {
        Lives = Balance.MaxLives;
        Coins = 0;
        Distance = 0f;
        CurrentState = State.Menu;
        hud?.Init(Lives, Coins, Distance, CurrentState);
    }

    public void AddCoins(int amount) {
        Coins += amount;
        hud?.SetCoins(Coins);
    }

    public void HitObstacle() {
        if (CurrentState != State.Playing) return;
        Lives = Mathf.Max(0, Lives - 1);
        hud?.SetLives(Lives);
        if (Lives <= 0) {
            CurrentState = State.GameOver;
            hud?.SetState(CurrentState);
        }
    }
}
