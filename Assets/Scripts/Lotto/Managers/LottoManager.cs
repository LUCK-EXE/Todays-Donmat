using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LottoManager : MonoBehaviour
{

    [Header("설정값")]
    public int ticketPrice = 10000;
    public int numbersToChoose = 4;
    public int minNumber = 1;
    public int maxNumber = 20;
    public int mainDrawCount = 5; // 메인 당첨 번호 개수  

    [Header("UI")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI playsLeftText;
    public TextMeshProUGUI ticketPriceText;
    public TextMeshProUGUI selectedNumbersText;
    public TextMeshProUGUI resultText;
    public Button playButton;
    public TextMeshProUGUI drawResultText;

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
        moneyText.text = $"남은 돈: {data.Money:N0}";
        playsLeftText.text = $"남은 게임 수: {data.PlaysLeft}";
    }

    private void RefreshTicketUI()
    {
        if (ticketPriceText == null) return;
        ticketPriceText.text = $"티켓 가격: {ticketPrice:N0}";
    }

    private void RefreshSelectedNumbersText()
    {
        if (selectedNumbersText == null) return;

        if (selectedNumbers.Count == 0)
        {
            selectedNumbersText.text = "선택된 숫자들:";
        }
        else
        {
            selectedNumbersText.text =
                "선택된 숫자들: " + string.Join(", ", selectedNumbers.OrderBy(x => x));
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
            resultText.text = $"반드시 {numbersToChoose}개의 숫자를 선택해야 합니다.";
            return;
        }

        // 2. 오늘 남은 게임 수 체크
        if (!gm.TryUsePlay())
        {
            if (resultText != null)
                resultText.text = "오늘은 더 이상 게임을 할 수 없습니다.";
            return;
        }

        // 3. 티켓 가격만큼 돈 사용 시도
        if (!gm.TrySpendMoney(ticketPrice))
        {
            if (resultText != null)
                resultText.text = "돈이 부족합니다.";
            return;
        }
        // 4. 추첨 + 등수 + 배당금 계산
        PlayRoundAndResolveReward();

        // 한 판 끝난 뒤 버튼 상태 재계산
        UpdatePlayButtonInteractable();
    }
    public void OnClickBackToMain()
    {
        SceneManager.LoadScene("LobbyScene");
    }
    public void OnClickAutoPick()
    {
        // 1. 먼저 한 판 초기화 (선택 번호 리셋)
        selectedNumbers.Clear();
        foreach (var btn in numberButtons)
        {
            btn.SetSelected(false);
            btn.ResetColor();
        }

        // 2. 번호 풀(1~20)에서 랜덤 4개 뽑기
        List<int> pool = Enumerable.Range(minNumber, maxNumber - minNumber + 1).ToList();

        // 간단한 셔플
        for (int i = 0; i < pool.Count; i++)
        {
            int randIndex = Random.Range(i, pool.Count);
            (pool[i], pool[randIndex]) = (pool[randIndex], pool[i]);
        }

        var picked = pool.Take(numbersToChoose).ToList();

        // 3. 뽑힌 번호 선택 적용
        foreach (int number in picked)
        {
            selectedNumbers.Add(number);
            // 버튼 찾아서 선택 표시
            var btn = numberButtons.First(b => b.number == number);
            btn.SetSelected(true);
        }

        // 4. UI 업데이트
        RefreshSelectedNumbersText();
        UpdatePlayButtonInteractable();
    }

    private void PlayRoundAndResolveReward()
    {
        // 1. 1~20 숫자 풀 만들기
        List<int> pool = Enumerable.Range(minNumber, maxNumber - minNumber + 1).ToList();

        // 2. 간단 셔플
        for (int i = 0; i < pool.Count; i++)
        {
            int randIndex = Random.Range(i, pool.Count);
            (pool[i], pool[randIndex]) = (pool[randIndex], pool[i]);
        }

        // 3. 앞에서 mainDrawCount개 = 메인 당첨 번호
        List<int> mainNumbers = pool.Take(mainDrawCount).OrderBy(x => x).ToList();

        // 4. 그 다음 1개 = 보너스 번호
        int bonusNumber = pool[mainDrawCount];

        // 5. 일치 개수 계산
        int mainMatches = selectedNumbers.Count(n => mainNumbers.Contains(n));
        bool bonusMatch = selectedNumbers.Contains(bonusNumber);

        // 6. 등수 & 배당금 계산
        int rewardMultiplier = 0;
        string rankText = "낙첨";

        if (mainMatches == 4)
        {
            rankText = "1등";
            rewardMultiplier = 30; // 3000%
        }
        else if (mainMatches == 3 && bonusMatch)
        {
            rankText = "2등";
            rewardMultiplier = 10; // 1000%
        }
        else if (mainMatches == 3)
        {
            rankText = "3등";
            rewardMultiplier = 5; // 500%
        }
        else if (mainMatches == 2)
        {
            rankText = "4등";
            rewardMultiplier = 1; // 원금
        }

        int reward = ticketPrice * rewardMultiplier;

        // 7. 배당금 지급
        if (reward > 0 && GameManager.Instance != null)
        {
            GameManager.Instance.AddMoney(reward);
        }

        //  8. UI에 당첨 번호 / 결과 표시
        HighlightResultNumbers(mainNumbers, bonusNumber);

        if (drawResultText != null)
        {
            string mainStr = string.Join(", ", mainNumbers);
            drawResultText.text = $"당첨 번호: {mainStr} / 보너스 번호: {bonusNumber}";
        }

        if (resultText != null)
        {
            string selectedStr = string.Join(", ", selectedNumbers.OrderBy(x => x));
            string matchInfo = $"맞춘 번호: {mainMatches}개" + (bonusMatch ? " + 보너스" : "");

            string rewardInfo = reward > 0
                ? $"보상: {reward:N0}"
                : "보상 없음";
            resultText.text =
                $"당신의 숫자들: {selectedStr}\n" +
                $"결과: {rankText} ({matchInfo})\n" +
                rewardInfo;
        }
    }

    public void ResetRound()
    {
        // 선택 번호 초기화
        selectedNumbers.Clear();

        // 버튼 상태 초기화
        foreach (var btn in numberButtons)
        {
            btn.SetSelected(false);
            btn.ResetColor();
        }

        // UI 초기화
        if (selectedNumbersText != null)
            selectedNumbersText.text = "선택된 숫자들: 없음";

        if (drawResultText != null)
            drawResultText.text = "당첨 번호가 여기에 표시됩니다";

        if (resultText != null)
            resultText.text = "";

        // 플레이 버튼 비활성화
        UpdatePlayButtonInteractable();
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

    private void HighlightResultNumbers(List<int> mainNumbers, int bonusNumber)
    {
        foreach (var btn in numberButtons)
        {
            // 메인 당첨 번호
            if (mainNumbers.Contains(btn.number))
            {
                // 내가 선택한 번호였고, 메인 번호와 일치하면 = 맞춘 번호!
                if (selectedNumbers.Contains(btn.number))
                {
                    btn.SetHighlight(btn.matchedColor); // 초록색
                }
                else
                {
                    btn.SetHighlight(btn.winColor); // 노란색
                }
            }
            // 보너스 번호
            else if (btn.number == bonusNumber)
            {
                // 내가 선택했고 보너스 번호 맞춘 경우
                if (selectedNumbers.Contains(btn.number))
                {
                    btn.SetHighlight(btn.matchedColor);  // 초록색
                }
                else
                {
                    btn.SetHighlight(btn.bonusColor); // 빨간색
                }
            }
            else
            {
                btn.ResetColor();  // 일반 번호
            }
        }
    }
}
