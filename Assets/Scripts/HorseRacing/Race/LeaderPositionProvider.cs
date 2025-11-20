using UnityEngine;

public class LeaderPositionProvider : MonoBehaviour
{
    [SerializeField]
    private RaceManager raceManager;

    public Transform GetPosition()
    {
        Horse horse = raceManager.leaderHorse;
        return horse.transform;
    }

}
