using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceResultUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private TextMeshProUGUI bettingText;
    [SerializeField] private TextMeshProUGUI earningText;
    [SerializeField] private TextMeshProUGUI totalMoneyText;

    public void ShowResult(bool isWin, string horseName, int reward, int totalMoney)
    {
        Debug.Log("종료패널 로직 작동");
        panel.SetActive(true);

        winnerText.text = $"우승 말: {horseName}";
        bettingText.text = "배팅 결과: " + makeBettingText(isWin);
        earningText.text = $"배당금: {reward}";
        totalMoneyText.text = $"총 보유 금액: {totalMoney}";
    }

    private string makeBettingText(bool isWin)
    {
        if (isWin)
        {
           return "승리!";
        }
        return "패배…";
        
    }

    public void OnClickBackToLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
