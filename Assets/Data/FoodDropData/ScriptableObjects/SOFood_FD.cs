using UnityEngine;

[CreateAssetMenu(fileName = "New FoodConfig", menuName = "Foods/Food Config", order = 0)]
public class SOFood_FD : ScriptableObject
{
    [Header("Configuration")]
    public Sprite sprite;
    public int scorePoints;
}
