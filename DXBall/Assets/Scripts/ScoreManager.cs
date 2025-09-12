using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    int score = 0;

    public void addScore(int input)
    {
        if (input == 1) {
            score = score + input;
            scoreText.text = "Score: " + score.ToString();
        }
        else if (input == 0) {
            winText.text = "Game Over!";
        }
    }
}
