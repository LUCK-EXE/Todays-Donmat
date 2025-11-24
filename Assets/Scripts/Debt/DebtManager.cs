using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

        if (data.Debt <= 0)
        {
            resultText.text = "모든 부채를 상환했습니다!";
        }
    }

    public void OnClickPay10K()
    {
        TryPay(10_000);
    }

    public void OnClickPay100K()
    {
        TryPay(100_000);
    }

    public void OnClickPayAll()
    {
        if (GameManager.Instance == null) return;
        int allDebt = GameManager.Instance.Data.Debt;
        TryPay(allDebt);
    }

    private void TryPay(int amount)
    {
        if (GameManager.Instance == null) return;

        var data = GameManager.Instance.Data;

        if (data.Money <= 0)
        {
            resultText.text = "상환할 돈이 없습니다.";
            return;
        }

        if (data.Money < amount)
        {
            resultText.text = "보유 자산이 부족합니다. 가능한 범위 내에서 상환합니다.";
        }

        GameManager.Instance.PayDebt(amount);

        if (data.Debt <= 0)
        {
            resultText.text = "모든 부채를 상환했습니다!";
        }
        else
        {
            resultText.text = $"{amount:N0}원 상환 완료.";
        }
    }

    public void OnClickBackToLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
