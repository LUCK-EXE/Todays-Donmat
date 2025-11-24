using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BettingManager : MonoBehaviour
{
    [SerializeField] private HorseSelectionManager selectionManager;
    [SerializeField] private TMP_InputField bettingInputField;
    [SerializeField] private RaceStartData raceStartData;


    [SerializeField] private TextMeshProUGUI warningText;

    public void OnClickBettingButton()
    {
        if (!CanStartRace()) return;
        HorseData selectedHorse = FetchSelectedHorse();
        if (selectedHorse == null) return;

        int amount = FetchBettingAmount();
        if (amount <= 0) return;


        if (!TryUseMoney(amount)) return;

        // 경마 시작 데이터 설정
        raceStartData.selectedHorse = selectedHorse;
        raceStartData.bettingAmount = amount;

        SceneManager.LoadScene("HorseRacingScene");
    }
    private bool CanStartRace()
    {
        if (GameManager.Instance.GetPlaysLeft() <= 0)
        {
            ShowWarning("오늘의 게임 횟수를 모두 소모했습니다!");
            return false; 
        }
        return true;
    }

    private HorseData FetchSelectedHorse()
    {
        HorseData horse = selectionManager.GetSelectedHorse();

        if (horse == null)
        {
            ShowWarning("말이 선택되지 않았습니다!");
            return null;
        }

        return horse;
    }

    private int FetchBettingAmount()
    {
        if (!int.TryParse(bettingInputField.text, out int amount) || amount <= 0)
        {
            ShowWarning("올바른 금액을 입력해주세요.");
            return -1;
        }

        return amount;
    }

    private bool TryUseMoney(int amount)
    {
        if (!GameManager.Instance.TrySpendMoney(amount))
        {
            ShowWarning("잔액이 부족합니다.");
            return false;
        }

        return true;
    }

    private void ShowWarning(string message)
    {
        if (warningText != null)
        {
            warningText.text = message;
        }
        Debug.LogWarning(message);
    }
}
