using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HorseItemController : MonoBehaviour
{
    public Image portraitImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI bettingRateText;
    public Button selectButton;

    private HorseData horseData;
    private HorseSelectionManager selectionManager;

    public void Setup(HorseData data, HorseSelectionManager manager)
    {
        horseData = data;
        selectionManager = manager;

        portraitImage.sprite = horseData.portrait;
        nameText.text = horseData.horseName;
        speedText.text = $"속도: {horseData.speed}";
        descriptionText.text = horseData.description;
        bettingRateText.text = $"배당률: {horseData.bettingRate}배";

        selectButton.onClick.AddListener(OnSelectClicked);
    }

    private void OnSelectClicked()
    {
        selectionManager.SelectHorse(this);
    }

    public HorseData GetHorseData()
    {
        return horseData;
    }

    //public void SetSelectedUI(bool isSelected)
    //{
    //    // 선택되면 버튼 색 바꾸기
    //    ColorBlock cb = selectButton.colors;
    //    if (isSelected)
    //    {
    //        cb.normalColor = Color.gray;
    //    }
    //    if (!isSelected)
    //    {
    //        cb.normalColor = Color.white;
    //    }
    //    selectButton.colors = cb;
    //}
}
