using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public HorseData data;

    private float currentSpeed;

    private void Start()
    {
        currentSpeed = data.speed;
    }
}
