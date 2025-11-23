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

    public void Setup(HorseData horseData)
    {
        portraitImage.sprite = horseData.portrait;
        nameText.text = horseData.horseName;
        speedText.text = $"속도: {horseData.speed}";
        descriptionText.text = horseData.description;
        bettingRateText.text = $"배당률: {horseData.bettingRate}배";
    }

    // TODO: 버튼 기능 구현
}
