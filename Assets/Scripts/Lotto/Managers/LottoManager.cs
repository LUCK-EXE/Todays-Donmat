using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class LottoManager : MonoBehaviour
{
    [Header("설정값")]
    public int numbersToChoose = 4;

    [Header("UI")]
    public TextMeshProUGUI selectedNumbersText;

    [Header("번호 버튼들")]
    public List<LottoNumberButton> numberButtons;

    private List<int> selectedNumbers = new List<int>();

    private void Start()
    {
        // 버튼들에 매니저 연결 & 초기화
        foreach (var btn in numberButtons)
        {
            if (btn == null) continue;
            btn.lottoManager = this;
            btn.SetSelected(false);
        }

        RefreshSelectedNumbersText();
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

    private void RefreshSelectedNumbersText()
    {
        if (selectedNumbers.Count == 0)
        {
            selectedNumbersText.text = "Selected numbers: None";
        }
        else
        {
            selectedNumbersText.text =
                "Selected numbers: " + string.Join(", ", selectedNumbers.OrderBy(x => x));
        }
    }
}
