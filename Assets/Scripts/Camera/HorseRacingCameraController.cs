using UnityEngine;

public class HorseRacingCameraController : MonoBehaviour
{
    [SerializeField]
    private RaceManager raceManager;
    private float smoothTime = 0.3f; // 클수록 느리게, 작을수록 빠르게 카메라가 따라옴

    private Vector3 cameraVelocity = Vector3.zero;

    void LateUpdate() // 말 이동(Update)이후 카메라 이동(LateUpdate)
    {
        Horse leader = raceManager.leaderHorse;
        Transform target = leader.transform;

        Vector3 desiredPosition = new Vector3(
            target.position.x,
            transform.position.y,
            transform.position.z
        );

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref cameraVelocity,
            smoothTime
        );
    }
}
