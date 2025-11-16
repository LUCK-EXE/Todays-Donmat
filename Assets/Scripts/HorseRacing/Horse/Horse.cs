using UnityEngine;

public class Horse : MonoBehaviour
{
    public HorseData data;

    private float currentSpeed;

    private void Start()
    {
        currentSpeed = data.speed;
    }
}
