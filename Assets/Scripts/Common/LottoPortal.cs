using UnityEngine;
using UnityEngine.SceneManagement;

public class LottoPortal : MonoBehaviour
{
    public string sceneName = "LottoScene";

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 부딪힌 애의 Tag가 "Player" 이면
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}