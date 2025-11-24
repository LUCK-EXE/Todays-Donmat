public class DebtManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI debtText;
    public TextMeshProUGUI resultText;

    private void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnGameStateChanged += RefreshUI;

        RefreshUI();
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnGameStateChanged -= RefreshUI;
    }

    private void RefreshUI()
    {
        if (GameManager.Instance == null) return;

        var data = GameManager.Instance.Data;

        moneyText.text = $"보유 자산: {data.Money:N0}원";
        debtText.text = $"남은 부채: {data.Debt:N0}원";
    }
}
