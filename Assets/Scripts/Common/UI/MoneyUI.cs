using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start()
    {
        // 게임 상태 변경될 때마다 UI 갱신하는 코드
        GameManager.Instance.OnGameStateChanged += RefreshUI;
        RefreshUI();
    }

    private void OnDestroy()
    {
        // 씬이 바뀌거나 오브젝트가 삭제될 때 이벤트 구독 해제
        if (GameManager.Instance != null)
            GameManager.Instance.OnGameStateChanged -= RefreshUI;
    }

    private void RefreshUI()
    {
        int money = GameManager.Instance.GetMoney();
        moneyText.text = $"보유 금액: {money:N0}원";
    }
}
