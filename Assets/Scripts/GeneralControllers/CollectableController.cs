using UnityEngine;

public enum CollectableType
{
    Food,
    InedibleFood,
    Coin,
    Danger,
    Fence
}

public class CollectableController : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private int scorePoints;
    [SerializeField] private GameType gameType;
    [SerializeField] private CollectableType collectableType;

    public void SetScore()
    {
        switch (gameType)
        {
            case GameType.FoodDrop:
                if (collectableType == CollectableType.Food)
                {
                    GCFoodDrop.Instance.SetScore(scorePoints);
                }
                else if (collectableType == CollectableType.InedibleFood)
                {
                    GCFoodDrop.Instance.SetScore(scorePoints * -1);
                }
                else
                {
                    Debug.Log("Não existe esse tipo de coletável nesse jogo");
                }
                break;

            case GameType.HealthGame:
                GCHealthGame.Instance.SetScore(scorePoints);
                break;

            case GameType.HillDrive:
                GCHillDrive.Instance.SetScore(scorePoints);
                break;

            case GameType.Runner:
                if (collectableType == CollectableType.Coin)
                {
                    GCRunner.Instance.SetScore(scorePoints);
                }
                else if (collectableType == CollectableType.Fence)
                {
                    GCRunner.Instance.SetScore(scorePoints * -1);
                    Debug.Log("Retirando " + scorePoints);
                    Animator animator = GetComponent<Animator>();
                    animator.SetBool("destroyed", true);
                }
                else
                {
                    Debug.Log("Não existe esse tipo de coletável nesse jogo");
                }
                break;
        }
    }

    public void SetLife ()
    {
        switch (gameType)
        {
            case (GameType.Runner):
                GCRunner.Instance.SetPlayerLives(-1);
                break;
        }
            

    }
}
