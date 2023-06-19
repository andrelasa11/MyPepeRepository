using UnityEngine;

public class PepperController : MonoBehaviour
{
    [Header("Configuration")]
    public int scorePoints;

    public void AddScore() => GCFoodDrop.Instance.SetScore(scorePoints);

    public void SetOnFireMode() => GCFoodDrop.Instance.SetOnFire();
}
