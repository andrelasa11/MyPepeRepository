using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    [Header("Configuration")]
    public int scorePoints;

    //[Header("Dependencies")]
    //[SerializeField] private SpriteRenderer spriteRenderer;
    //[SerializeField] private List<SOFood_FD> foodConfigs;

    //private
    //private int foodConfigIndex;

    // Start is called before the first frame update
    void Start()
    {
        /*if (foodConfigs != null)
        {
            foodConfigIndex = Random.Range(0, foodConfigs.Count);

            spriteRenderer.sprite = foodConfigs[foodConfigIndex].sprite; // Instanciamento do SO;
            scorePoints = foodConfigs[foodConfigIndex].scorePoints; // Instanciamento do SO;
        }*/
    }

    public void AddScore() => GCFoodDrop.Instance.SetScore(scorePoints);

    public void DecreaseLife() => GCFoodDrop.Instance.SetPlayerLives(-1);

}
