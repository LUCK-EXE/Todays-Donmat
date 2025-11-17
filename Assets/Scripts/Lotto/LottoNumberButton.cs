using UnityEngine;
using UnityEngine.UI;

public class LottoNumberButton : MonoBehaviour
{
    public int number;
    public LottoManager lottoManager;

    [Header("색상 설정")]
    public Color normalColor = Color.white;
    public Color selectedColor = new Color(0.8f, 0.8f, 0.8f);

    private Button button;
    private bool isSelected = false;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (lottoManager == null) return;
        lottoManager.ToggleNumber(number, this);
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;

        var colors = button.colors;

        if (selected)
        {
            colors.normalColor = selectedColor;
            colors.highlightedColor = selectedColor;
            colors.selectedColor = selectedColor;
        }
        else
        {
            colors.normalColor = normalColor;
            colors.highlightedColor = normalColor;
            colors.selectedColor = normalColor;
        }

        button.colors = colors;
    }

    public bool IsSelected() => isSelected;
}
