using UnityEngine;

public class CameraSmoothFollow : MonoBehaviour
{
    public Transform target { get; private set; }

    private float smoothTime = 0.3f; // 클수록 느리게, 작을수록 빠르게 카메라가 따라옴
    private Vector3 cameraVelocity = Vector3.zero;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void LateUpdate()
    {

        if (target == null)
        {
            return;
        }

        Vector3 desiredPosition = SetDesiredPosition();

        MoveSmooth(desiredPosition);
    }

    private Vector3 SetDesiredPosition()
    {
        Vector3 desiredPosition = new Vector3(
            target.position.x,
            transform.position.y,
            transform.position.z
        );

        return desiredPosition;
    }

    private void MoveSmooth(Vector3 desiredPosition)
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref cameraVelocity,
            smoothTime
        );
    }
}
