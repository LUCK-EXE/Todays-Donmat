using System.Collections.Generic;
using UnityEngine;

public class HorseSelectionManager : MonoBehaviour
{
    // Instantiate 시 코드에서 Add하기
    public List<HorseItemController> horseItems = new List<HorseItemController>();

    private HorseItemController selectedItem;  // 현재 선택된 카드 정보

    public void SelectHorse(HorseItemController item)
    {
        // 기존 선택이 있으면 UI 끔
        if (selectedItem != null)
        {
            selectedItem.SetSelectedUI(false);
        }

        // 새로운 선택 UI 켬
        selectedItem = item;
        selectedItem.SetSelectedUI(true);

        Debug.Log($"경주마 {selectedItem.GetHorseData().horseName}가 선택됨");
    }

    // 선택된 말을 다른 곳에 전달하는 메소드
    public HorseData GetSelectedHorse()
    {
        if (selectedItem == null)
            return null;

        return selectedItem.GetHorseData();
    }
}
