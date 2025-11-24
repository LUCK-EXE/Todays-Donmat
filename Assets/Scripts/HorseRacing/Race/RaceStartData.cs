using UnityEngine;

[CreateAssetMenu(fileName = "RaceStartData", menuName = "Scriptable Objects/RaceStartData")]
public class RaceStartData : ScriptableObject
{
    public HorseData selectedHorse;
    public int bettingAmount;
}
