using System;
using UnityEngine;

public abstract class GameController : MonoBehaviour
{

    [Header("Data")]
    public int score;
    public float distance;
    public int playerLife;
    public float totalScore;

    [Header("Dependencies")]
    [SerializeField] protected GameObject mainCanvas;
    [SerializeField] protected GameObject gameOverCanvas;

    [Header("UI")]
    [SerializeField] protected DistanceUI distanceUI;
    [SerializeField] protected ScoreUI scoreUI;
    [SerializeField] protected LifeUI lifeUI;

    public virtual void SetScore(int scorePoints)
    {
        if(scorePoints > 0)
        {
            score += scorePoints;
            AudioManager.Instance.PlayCoin();
            scoreUI.SetScoreValueText(score);
        }
        else
        {
            score += scorePoints;
            scoreUI.SetScoreValueText(score);
        }
        
    }

    public virtual void SetPlayerLives(int value)
    {
        playerLife += value;
        AudioManager.Instance.PlayFailure();
        lifeUI.SetValueText(playerLife);

        if (playerLife <= 0)
        {
            OnGameOver();
        }
    }

    public virtual void OnGameOver()
    {   
        Time.timeScale = 0;

        scoreUI.SetTotalValueText(score, totalScore);

        mainCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
    }

}
