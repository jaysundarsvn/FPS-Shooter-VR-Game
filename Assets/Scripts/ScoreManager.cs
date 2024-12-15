using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score, highScore;
    public TextMeshProUGUI scoreText, scoreNumber;
    // Start is called before the first frame update
   
    public void SetHighScore()
    {
        if(score > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", score);
        }

        highScore = PlayerPrefs.GetInt("highscore");

        scoreNumber.text = "" + highScore;
        scoreText.text = "High Score:";
    }

    public void ResetScore()
    {
        score = 0;
        scoreNumber.text = "" + score;
        scoreText.text = "Score:";
    }

    public void PointsToAdd(int points_to_add)
    {
        score += points_to_add;
        scoreNumber.text += "" + score;
    }
}
