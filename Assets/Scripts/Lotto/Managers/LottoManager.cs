using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LottoManager : MonoBehaviour
{
    [Header("설정값")]
    public int ticketPrice = 10000;
    public int numbersToChoose = 4;

    [Header("UI")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI playsLeftText;
    public TextMeshProUGUI ticketPriceText;
    public TextMeshProUGUI selectedNumbersText;
    public TextMeshProUGUI resultText;
    public Button playButton;

    [Header("번호 버튼들")]
    public List<LottoNumberButton> numberButtons;

    private readonly List<int> selectedNumbers = new List<int>();

    private void OnEnable()
    {
        // GameManager 상태 변경 이벤트 구독
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStateChanged += RefreshTopUI;
        }

        // 씬에 처음 들어올 때도 한 번 갱신
        RefreshTopUI();
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStateChanged -= RefreshTopUI;
        }
    }

    private void Start()
    {
        // 번호 버튼 초기화
        foreach (var btn in numberButtons)
        {
            if (btn == null) continue;
            btn.lottoManager = this;
            btn.SetSelected(false);
        }

        RefreshTicketUI(); // 티켓 가격 표시
        RefreshSelectedNumbersText();

        if (resultText != null) resultText.text = "";

        // 처음에는 번호 4개 선택 전이니까 버튼은 일단 비활성화해도 됨
        UpdatePlayButtonInteractable();
    }

    private void RefreshTopUI()
    {
        if (GameManager.Instance == null) return;

        var data = GameManager.Instance.Data;
        moneyText.text = $"Money {data.Money:N0}";
        playsLeftText.text = $"{data.PlaysLeft} game left";
    }

    private void RefreshTicketUI()
    {
        if (ticketPriceText == null) return;
        ticketPriceText.text = $"Ticket price: {ticketPrice:N0}";
    }

    private void RefreshSelectedNumbersText()
    {
        if (selectedNumbersText == null) return;

        if (selectedNumbers.Count == 0)
        {
            selectedNumbersText.text = "Selected numbers: None";
        }
        else
        {
            selectedNumbersText.text =
                "Selected numbers: " + string.Join(", ", selectedNumbers.OrderBy(x => x));
        }

        // 번호 선택 개수에 따라 플레이 버튼 활성/비활성
        UpdatePlayButtonInteractable();
    }

    private void UpdatePlayButtonInteractable()
    {
        if (playButton == null) return;

        playButton.interactable = (selectedNumbers.Count == numbersToChoose);
    }

    public void ToggleNumber(int number, LottoNumberButton button)
    {
        if (selectedNumbers.Contains(number))
        {
            selectedNumbers.Remove(number);
            button.SetSelected(false);
        }
        else
        {
            if (selectedNumbers.Count >= numbersToChoose)
            {
                Debug.Log($"최대 {numbersToChoose}개까지만 선택 가능");
                return;
            }

            selectedNumbers.Add(number);
            button.SetSelected(true);
        }

        RefreshSelectedNumbersText();
    }

    public void OnClickPlay()
    {
        var gm = GameManager.Instance;
        if (gm == null) return;

        var data = gm.Data;

        // 1. 번호 4개 다 골랐는지 체크
        if (selectedNumbers.Count != numbersToChoose)
        {
            resultText.text = $"You must select all {numbersToChoose} numbers.";
            return;
        }

        // 2) 오늘 남은 게임 수 체크
        if (!gm.TryUsePlay())
        {
            if (resultText != null)
                resultText.text = "You can't play any more games today.";
            return;
        }

        // 3) 티켓 가격만큼 돈 사용 시도
        if (!gm.TrySpendMoney(ticketPrice))
        {
            if (resultText != null)
                resultText.text = "You don't have enough money.";
            return;
        }

        if (resultText != null)
            resultText.text = "Ticket purchased. (Drawing logic will be implemented next)";
            ClearSelection();
    }

    public void ClearSelection()
    {
        selectedNumbers.Clear();

        foreach (var btn in numberButtons)
        {
            if (btn == null) continue;
            btn.SetSelected(false);
        }

        RefreshSelectedNumbersText();
    }
}
