using UnityEngine;

public class InedibleController : MonoBehaviour
{

    [Header("Configuration")]
    public int scoreToDecrease;

    public void DecreaseScore() => GCFoodDrop.Instance.SetScore(scoreToDecrease * -1);

    public void DecreaseLife() => GCFoodDrop.Instance.SetPlayerLives(-1);

}
