using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UI_Title : MonoBehaviour
{
    UI_Title_Button_GameStart buttonGameStart = null;
    private void Awake()
    {
        buttonGameStart = GetComponentInChildren<UI_Title_Button_GameStart>();
    }

    private void Start()
    {
        SetButtonEvent();
    }

    public void SetButtonEvent()
    {
        buttonGameStart.SetEvent(ChangeScene);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
