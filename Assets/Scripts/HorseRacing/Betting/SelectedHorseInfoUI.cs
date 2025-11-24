using TMPro;
using UnityEngine;

public class SelectedHorseInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;

    public void UpdateInfo(HorseData horse)
    {
        nameText.text = $"선택된 경주마: {horse.horseName}";
    }
}
