using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button restartGame;
    [SerializeField] private Button quitGame;

    private void Start()
    {
        // Ẩn bảng Game Over khi bắt đầu game
        gameOverPanel.SetActive(false);
        victoryPanel.gameObject.SetActive(false);
        // Gán sự kiện cho nút
        restartGame.onClick.AddListener(RestartGame);
        quitGame.onClick.AddListener(QuitGame);
    }

    // Gọi khi Player chết
    public void GameOver()
    {
        AudioManager.Instance.PlayVFX("GameOver");
        gameOverPanel.SetActive(true);
        scoreText.text = "Score: 0"; // Có thể thay bằng điểm thực tế

        // Hiện lại chuột khi game over
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void GameOverWithDelay(float delay)
    {
        Invoke("GameOver", delay);
    }

    public void Victory()
    {
        AudioManager.Instance.PlayVFX("Victory");
        victoryPanel.SetActive(true);
        scoreText.text = "Score: 0"; // Có thể thay bằng điểm thực tế
                                     // Hiện lại chuột khi chiến thắng
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Restart game
    public void RestartGame()
    {
        AudioManager.Instance.PlayVFX("Click UI");
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Thoát game
    public void QuitGame()
    {
        AudioManager.Instance.PlayVFX("Click UI");
        Application.Quit();
    }
}
