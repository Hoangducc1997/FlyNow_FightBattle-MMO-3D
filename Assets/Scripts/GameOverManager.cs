using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Button restartGame;
    [SerializeField] Button quitGame;

    private void Start()
    {
        //Ẩn panel game over
        gameOverPanel.SetActive(false);
        //Gán sự kiện click cho nút restart game
        restartGame.onClick.AddListener(RestartGame);
        quitGame.onClick.AddListener(QuitGame);
    }
    //Restart game
    public void RestartGame()
    {
        //Ẩn panel game over
        gameOverPanel.SetActive(false);
        //Load lại scene hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //Thoát game
    public void QuitGame()
    {
        //Thoát game
        Application.Quit();
    }
    //Máu người chơi < 0
    public void OnPlayerDead(PlayerInfo playerInfo)
    {
        if (playerInfo != null && playerInfo.GetCurrentHealth() <= 0)
        {
            GameOver(playerInfo);
        }
    }

    //Gọi hàm này khi game over
    public void GameOver(PlayerInfo playerInfo)
    {
        gameOverPanel.SetActive(true);
        scoreText.text = "Score: 0";
    }
}
