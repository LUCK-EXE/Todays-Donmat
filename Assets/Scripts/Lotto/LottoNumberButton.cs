using UnityEngine;
using UnityEngine.UI;

public class LottoNumberButton : MonoBehaviour
{
    public int number;
    public LottoManager lottoManager;

    [Header("색상 설정")]
    public Color normalColor = Color.white;
    public Color selectedColor = new Color(0.8f, 0.8f, 0.8f);
    public Color winColor = new Color(1f, 0.9f, 0.4f);    // 노란색
    public Color bonusColor = new Color(1f, 0.5f, 0.5f);  // 붉은 색
    public Color matchedColor = new Color(0.5f, 1f, 0.5f); // 녹색 

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

    public void SetHighlight(Color color)
    {
        var colors = button.colors;
        colors.normalColor = color;
        colors.highlightedColor = color;
        colors.selectedColor = color;
        button.colors = colors;
    }

    public void ResetColor()
    {
        SetHighlight(normalColor);
    }
}
