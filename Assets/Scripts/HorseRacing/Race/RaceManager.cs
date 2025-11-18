using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField]
    private List<Horse> horses;

    private bool raceFinished = false;

    void Start()
    {
        foreach (var horse in horses)
        {
            horse.OnHorseFinished += HandleHorseFinished; // 말이 결승선을 통과했을 때 알림 받기
        }
    }

    void Update()
    {
        
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

    private void EndRace()
    {
        // TODO: 경주 종료 처리 로직 추가
    }
}
