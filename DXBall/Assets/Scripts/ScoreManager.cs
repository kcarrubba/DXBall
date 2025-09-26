using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public GameObject ball;
    public bool gameOver = false;
    int score = 0;

    public void addScore(int input)
    {
        if (input == 1) {
            score = score + input;
            scoreText.text = "Score: " + score.ToString();
            if (score == 4) {
                winText.text = "You Win!";
                ball.SetActive(false);
                // Check which scene we’re in
                string currentScene = SceneManager.GetActiveScene().name;

                if (currentScene == "Level1")
                {
                    winText.text = "Level Complete!";
                    SceneManager.LoadScene("Level2");
                }
                else if (currentScene == "Level2")
                {
                    gameOver = true;
                    // Disable all balls
                    GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
                    foreach (GameObject b in balls)
                    {
                        b.SetActive(false);
                    }
                }
            }
        }
        else if (input == 0) {
            winText.text = "Game Over!";
            gameOver = true;

        }
    }
}
