using TMPro;
using UnityEngine;
public class Score : MonoBehaviour
{
    int score = 0;
    [SerializeField] TMP_Text scoreboardText;
    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreboardText.text = score.ToString();
    }
}
