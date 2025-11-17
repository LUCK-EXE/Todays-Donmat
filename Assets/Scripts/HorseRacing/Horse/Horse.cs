using UnityEngine;

public class Horse : MonoBehaviour
{
    private const float FINISH_LINE_X = 20f;
    
    public HorseData data;

    private float currentSpeed;

    private void Start()
    {
        currentSpeed = data.speed;
    }

    private void Update()
    {
        if (transform.position.x < FINISH_LINE_X)
        {
            transform.position += Vector3.right * currentSpeed * Time.deltaTime;
        }
    }
}
