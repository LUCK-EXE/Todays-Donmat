using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private List<Horse> horses;
    [SerializeField] private RaceStartData raceStartData;
    [SerializeField] private RaceResultUI raceResultUI;
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
            EndRace(finishedHorse);
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

    private void EndRace(Horse winningHorse)
    {
        bool win = IsWin(winningHorse);

        int bettingAmount = raceStartData.bettingAmount;
        int reward = calculateReward(bettingAmount, win);

        GameManager.Instance.AddMoney(reward);
        int totalMoney = GameManager.Instance.GetMoney();

        raceResultUI.ShowResult(
            win,
            winningHorse.data.horseName,
            reward,
            totalMoney
        );
    }

    private bool IsWin(Horse winningHorse)
    {
        if (winningHorse.data == raceStartData.selectedHorse)
        {
            return true;
        }
        return false;
    }

    private int calculateReward(int bettingAmount, bool win)
    {
        if (win)
        {
            float rate = raceStartData.selectedHorse.bettingRate;
            int reward = Mathf.RoundToInt(bettingAmount * rate);
            
            Debug.Log($"배팅 성공. {reward}원 획득");
            return reward;
        }
        
        Debug.Log("배팅 실패");
        return 0;
        
    }
}
