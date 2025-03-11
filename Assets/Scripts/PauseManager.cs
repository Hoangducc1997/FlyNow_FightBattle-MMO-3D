using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingPanel;

    void Start()
    {
        settingPanel.SetActive(false);
        exitButton.onClick.AddListener(ExitGame);
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game has exited."); // Chỉ hoạt động trong Editor
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Ngừng thời gian trong game
        Debug.Log("Game Paused.");
    }

    public void SettingGame()
    {
        settingPanel.SetActive(true);
        pausePanel.SetActive(false);
    }



    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Tiếp tục thời gian trong game
        Debug.Log("Game Resumed.");
    }
}
