using UnityEngine;
using TMPro;

public class LobbyUIController : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI playsLeftText;

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

        var money = GameManager.Instance.GetMoney();
        var playsLeft = GameManager.Instance.GetPlaysLeft();

        moneyText.text = $"남은 돈: {money:N0}";
        playsLeftText.text = $"남은 게임 수: {playsLeft}";
    }
}