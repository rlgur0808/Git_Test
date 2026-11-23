using UnityEngine;

public class UI_Score : MonoBehaviour
{
    // GameManager에서 점수를 세고 있음
    // UI_Manager를 통해서
    // UI_Score에 그 점수를 전달할거임
    // UI_Score는 자식에 있는 UI_Score_Text의
    // Text를 Score : 10 이런식으로 바꿔주면 됨
    private UI_Score_Text textScore = null;

    private void Awake()
    {
        textScore = GetComponentInChildren<UI_Score_Text>();
    }

    public void UpdateScore(int score)
    {
        textScore.SetText($"Score : {score}");
    }
}
