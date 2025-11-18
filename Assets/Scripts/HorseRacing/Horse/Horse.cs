using UnityEngine;

public class Horse : MonoBehaviour
{
    private const float FINISH_LINE_X = 20f;

    public HorseData data;
    private HorseMovementPattern movementPattern;
    private HorseConditionState conditionState;
    private RaceTrack raceTrack;
    private float currentSpeed;

    private void Start()
    {
        currentSpeed = data.speed;
        movementPattern = new HorseMovementPattern();
        conditionState = new HorseConditionState(data);
        raceTrack = new RaceTrack();
    }

    private void Update()
    {
        currentSpeed = conditionState.getTodaySpeed() + movementPattern.GetCurrentPattern();

        {
            transform.position += Vector3.right * currentSpeed * Time.deltaTime;
        }
    }
}
