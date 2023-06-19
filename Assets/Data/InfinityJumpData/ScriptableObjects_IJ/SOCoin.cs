using UnityEngine;

[CreateAssetMenu(fileName = "New CoinConfig", menuName = "Coins/Coin Config", order = 0)]
public class SOCoin : ScriptableObject
{
    [Header("Configuration")]
    public Sprite sprite;
    public int scorePoints;
}
