using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button restartGame;
    [SerializeField] private Button quitGame;

    private void Start()
    {
        // Ẩn bảng Game Over khi bắt đầu game
        gameOverPanel.SetActive(false);

        // Gán sự kiện cho nút
        restartGame.onClick.AddListener(RestartGame);
        quitGame.onClick.AddListener(QuitGame);
    }

    // Gọi khi Player chết
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        scoreText.text = "Score: 0"; // Có thể thay bằng điểm thực tế
    }
    public void GameOverWithDelay(float delay)
    {
        Invoke("GameOver", delay);
    }


    // Restart game
    public void RestartGame()
    {
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Thoát game
    public void QuitGame()
    {
        Application.Quit();
    }
}
