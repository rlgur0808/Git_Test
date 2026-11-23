using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UI_Score uiScore = null;
    private UI_GameClear uiGameClear = null;
    private UI_GameOver uiGameOver = null;

    private void Awake()
    {
        uiScore = GetComponentInChildren<UI_Score>();
        uiGameClear = GetComponentInChildren<UI_GameClear>();
        uiGameOver = GetComponentInChildren<UI_GameOver>();
    }
    public void Init()
    {
        uiGameClear.gameObject.SetActive(false);
        uiGameOver.gameObject.SetActive(false);
    }
    public void UpdateScoreUI(int score)
    {
        uiScore.UpdateScore(score);
    }
    public void ShowGameOverUI()
    {
        uiGameOver.gameObject.SetActive(true);
    }
    public void ShowGameClearUI()
    {
        uiGameClear.gameObject.SetActive(true);
    }
}
