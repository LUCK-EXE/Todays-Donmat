public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("초기 설정값")]
    public int initialMoney = 1_000_000;
    public int initialDebt = 5_000_000;
    public int maxPlaysPerDay = 3;
    [Range(0f, 1f)]
    public float interestRatePerDay = 0.05f; // 5%

    // 실제 현재 게임 상태
    public GameData Data { get; private set; }

    // public event Action OnGameStateChanged; 이렇게 선언해두고
    // OnGameStateChanged?.Invoke(); 호출 하고
    //
    // 예를 들어 아래와 같이 한다면
    // OnGameStateChanged += RefreshUI; 
    //
    // +=으로 구독한 메서드들이 호출됨 (다수의 메서드들 구독 가능)
    // RefreshUI();

    public event Action OnGameStateChanged;

    private void Awake()
    {
        // 싱글톤 패턴
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 최초 1회 초기화
        if (Data == null)
        {
            Data = new GameData(initialMoney, initialDebt, maxPlaysPerDay, interestRatePerDay);
            NotifyStateChanged();
        }
    }

    private void NotifyStateChanged()
    {
        OnGameStateChanged?.Invoke();
    }

    public bool TrySpendMoney(int amount)
    {
        bool result = Data.SpendMoney(amount);
        if (result)
        {
            NotifyStateChanged();
        }
        return result;
    }

    public void AddMoney(int amount)
    {
        Data.AddMoney(amount);
        NotifyStateChanged();
    }
}
