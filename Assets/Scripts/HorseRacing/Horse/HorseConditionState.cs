using UnityEngine;

public class HorseConditionState
{
    private float condition;
    private float todaySpeed;

    public HorseConditionState(HorseData data)
    {
        condition = GenereateCondition(data);
        todaySpeed = data.speed * condition;
    }

    public float getTodaySpeed()
    {
        return todaySpeed;
    }

    private float GenereateCondition(HorseData data)
    {
        return Random.Range(data.conditionMin, data.conditionMax);
    }
}
