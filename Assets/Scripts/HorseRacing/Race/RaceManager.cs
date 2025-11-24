using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private List<Horse> horses;
    [SerializeField] private RaceStartData raceStartData;
    public Horse leaderHorse {get; private set;}

    private bool raceFinished = false;

    void Start()
    {
        foreach (var horse in horses)
        {
            horse.OnHorseFinished += HandleHorseFinished; // 말이 결승선을 통과했을 때 알림 받기
        }

        HorseData selectedHorseData = raceStartData.selectedHorse;
        int bettingAmount = raceStartData.bettingAmount;

        Debug.Log($"선택된 말: {selectedHorseData.horseName}");
        Debug.Log($"베팅 금액: {bettingAmount}");

    }

    void Update()
    {
        leaderHorse = FindLeaderHorse();    
    }

    private void HandleHorseFinished(Horse finishedHorse)
    {
        if (!raceFinished)
        {
            raceFinished = true;
            Debug.Log($"경기 종료\n1등 말: {finishedHorse.name}");
            EndRace();
        }
    }

    private Horse FindLeaderHorse()
    {
        Horse leader = horses[0];
        foreach (var horse in horses)
        {
            if (horse.transform.position.x > leader.transform.position.x)
            {
                leader = horse;
            }
        }
        return leader;
    }

    private void EndRace()
    {
        // TODO: 경주 종료 처리 로직 추가
    }
}
