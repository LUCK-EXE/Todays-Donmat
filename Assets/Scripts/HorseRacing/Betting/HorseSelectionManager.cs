using System.Collections.Generic;
using UnityEngine;

public class HorseSelectionManager : MonoBehaviour
{
    // Instantiate 시 코드에서 Add하기
    public List<HorseItemController> horseItems = new List<HorseItemController>();
    private HorseItemController selectedItem;  // 현재 선택된 카드 정보

    [SerializeField] private SelectedHorseInfoUI horseInfoUI;

    public void SelectHorse(HorseItemController item)
    {

        selectedItem = item;
 
        // 선택된 말을 UI에 나타나게 반영
        horseInfoUI.UpdateInfo(item.GetHorseData());

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
