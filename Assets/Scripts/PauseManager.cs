﻿using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private GameObject pausePanel;

    void Start()
    {
        exitButton.onClick.AddListener(ExitGame);
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        settingButton.onClick.AddListener(SettingGame);
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
        Debug.Log("Đã gọi SettingGame");
        pausePanel.SetActive(false);
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Tiếp tục thời gian trong game
        Debug.Log("Game Resumed.");
    }
}
