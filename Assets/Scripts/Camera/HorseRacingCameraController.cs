using UnityEngine;

public class HorseRacingCameraController : MonoBehaviour
{
    [SerializeField]
    private LeaderPositionProvider leaderPositionProvider;

    [SerializeField]
    private CameraSmoothFollow smoothFollow;

    void LateUpdate()
    {
        Transform target = leaderPositionProvider.GetPosition();

        smoothFollow.SetTarget(target);
    }
}
