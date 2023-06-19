using UnityEngine;

[CreateAssetMenu(fileName = "New InedibleConfig", menuName = "Inedibles/Inedible Config", order = 0)]
public class SOInedible_FD : ScriptableObject
{
    [Header("Configuration")]
    public Sprite sprite;
    public int scoreToDecrease;
}
