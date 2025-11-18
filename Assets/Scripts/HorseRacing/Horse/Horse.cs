using UnityEngine;

public class Horse : MonoBehaviour
{
    private const float FINISH_LINE_X = 20f;

    public HorseData data;
    private HorseMovementPattern movementPattern;
    private HorseConditionState conditionState;
    private RaceTrack raceTrack;
    private float currentSpeed;
    private bool finish = false;

    private void Start()
    {
        currentSpeed = data.speed;
        movementPattern = new HorseMovementPattern();
        conditionState = new HorseConditionState(data);
        raceTrack = new RaceTrack();
    }

    private void Update()
    {
        if (finish)
        {
            return;
        }
        Move();
        CheckFinish();
    }

    private void Move()
    {
        currentSpeed = conditionState.getTodaySpeed() + movementPattern.GetCurrentPattern();
        transform.position += Vector3.right * currentSpeed * Time.deltaTime;
        
    }

    private void CheckFinish()
    {
        if(raceTrack.IsFinished(transform.position.x))
        {
            finish = true;
        }
    }
}
