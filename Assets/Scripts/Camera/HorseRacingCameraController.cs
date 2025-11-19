using UnityEngine;

public class HorseRacingCameraController : MonoBehaviour
{
    private float speed = 3f;

    void Update()
    {
        // TODO: 카메라 로직 구현 해야함
        transform.position += Vector3.right * speed * Time.deltaTime;
        Debug.Log("카메라 속도: " + speed);
    }
}
