using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 input;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 방향키 / WASD 입력 받기
        float x = Input.GetAxisRaw("Horizontal");   // 왼/오
        float y = Input.GetAxisRaw("Vertical");     // 위/아래

        input = new Vector2(x, y).normalized;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;
    }
}