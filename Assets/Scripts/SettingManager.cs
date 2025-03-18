using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private Button settingButton;
    private GameObject settingPanel;
    [SerializeField] private GameObject pausePanel;

    void Start()
    {
        settingButton.onClick.AddListener(SettingGame);

        // Tìm tất cả GameObject có Component và lọc theo tag "setting"
        foreach (GameObject obj in FindObjectsOfType<GameObject>(true))
        {
            if (obj.CompareTag("Setting"))
            {
                settingPanel = obj;
                break;
            }
        }

        if (settingPanel != null)
        {
            settingPanel.SetActive(false); // Đảm bảo ẩn lúc đầu
        }
        else
        {
            Debug.LogWarning("Không tìm thấy đối tượng có tag 'setting'");
        }
    }

    public void SettingGame()
    {
        if (settingPanel != null)
        {
            /*settingPanel.SetActive(true);*/
            UIManager.Instance.ShowPopup(PopupName.SettingPopup);
            AudioManager.Instance.PlayVFX("Click UI");
            Time.timeScale = 0f; // Ngừng thời gian trong game
            pausePanel.SetActive(false);
        }
    }
}
