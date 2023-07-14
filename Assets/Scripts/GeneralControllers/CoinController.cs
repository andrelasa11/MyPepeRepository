using UnityEngine;

public class CoinController : MonoBehaviour
{

    [Header("Configuration")]
    [SerializeField] private int scorePoints;
    [SerializeField] private GameType gameType;

    [Header("Dependencies")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void AddScore()
    {
        switch(gameType)
        {
            case GameType.HealthGame:
                GCHealthGame.Instance.SetScore(scorePoints);
                break;

            case GameType.HillDrive:
                GCHillDrive.Instance.SetScore(scorePoints);
                GCHillDrive.Instance.AddHappiness(1);
                break;

            case GameType.Runner:
                GCRunner.Instance.SetScore(scorePoints);
                break;
        }
    }

}