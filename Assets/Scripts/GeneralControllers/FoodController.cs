using UnityEngine;

public class FoodController : MonoBehaviour
{
    [Header("Configuration")]
    public int scorePoints;

    public void AddScore() => GCFoodDrop.Instance.SetScore(scorePoints);

    public void DecreaseLife() => GCFoodDrop.Instance.SetPlayerLives(-1);

}
