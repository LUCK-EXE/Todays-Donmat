using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FitBackgroundToCamera : MonoBehaviour
{
    private Camera cam;
    private SpriteRenderer sr;
    private float lastAspect;

    private void Awake()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Refit();
    }

    private void Update()
    {
        // 화면 비율이 바뀌었으면(= Game 뷰 크기를 바꿨으면) 다시 맞춤
        if (cam != null && !Mathf.Approximately(cam.aspect, lastAspect))
        {
            Refit();
        }
    }

    private void Refit()
    {
        if (cam == null || sr == null || sr.sprite == null) return;

        float worldScreenHeight = cam.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight * cam.aspect;

        Vector2 spriteSize = sr.sprite.bounds.size;

        float scaleX = worldScreenWidth / spriteSize.x;
        float scaleY = worldScreenHeight / spriteSize.y;

        transform.localScale = new Vector3(scaleX, scaleY, 1f);
        transform.position = new Vector3(0f, 0f, 0f);

        lastAspect = cam.aspect;
    }
}