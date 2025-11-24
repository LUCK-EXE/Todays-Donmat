using System.Collections.Generic;
using UnityEngine;

public class HorseListPanelController : MonoBehaviour
{
    [SerializeField] private List<HorseData> horseDataList;
    [SerializeField] private Transform upperRowParent;
    [SerializeField] private Transform lowerRowParent;
    [SerializeField] private HorseItemController horseItemPrefab;
    [SerializeField] private HorseSelectionManager selectionManager;

    void Start()
    {
        int upperRowCount = horseDataList.Count / 2;

        // 경주마가 홀수 일 땐 상단에 1개 적게 배치
        // 경주마가 짝수 일 땐 상단과 하단에 동일하게 배치
        for (int i = 0; i < upperRowCount; i++)
        {
            var item = Instantiate(horseItemPrefab, upperRowParent);
            item.Setup(horseDataList[i], selectionManager);
            selectionManager.horseItems.Add(item);
        }
        for (int i = upperRowCount; i < horseDataList.Count; i++)
        {
            var item = Instantiate(horseItemPrefab, lowerRowParent);
            item.Setup(horseDataList[i], selectionManager);
            selectionManager.horseItems.Add(item);
        }

    }
}
