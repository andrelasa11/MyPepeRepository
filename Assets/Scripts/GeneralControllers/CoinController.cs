using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    [Header("Configuration")]
    [SerializeField] private int scorePoints;
    [SerializeField] private GameType gameType;

    [Header("Dependencies")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    //[SerializeField] private List<SOCoin> coinConfigs;

    //private
   // private int coinConfigIndex;

    void Start()
    {
        //RandomConfig();
    }

    public void RandomConfig()
    {
       /* if(coinConfigs != null)
        {
            coinConfigIndex = Random.Range(0, coinConfigs.Count);

            spriteRenderer.sprite = coinConfigs[coinConfigIndex].sprite; // Instanciamento do SO;
            scorePoints = coinConfigs[coinConfigIndex].scorePoints; // Instanciamento do SO;
        }        */
    }

    public void AddScore()
    {
        switch(gameType)
        {
            case GameType.HealthGame:
                GCHealthGame.Instance.SetScore(scorePoints);
                break;

            case GameType.HillDrive:
                GCHillDrive.Instance.SetScore(scorePoints);
                break;

            case GameType.Runner:
                GCRunner.Instance.SetScore(scorePoints);
                break;
        }
    }

}