using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingPanel;

    void Start()
    {
        backButton.onClick.AddListener(BackPause);
    }

    public void BackPause()
    {
        Debug.Log("Đã gọi SettingGame");
        pausePanel.SetActive(true);
        settingPanel.SetActive(false);
    }

}
